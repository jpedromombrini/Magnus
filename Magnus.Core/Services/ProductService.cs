using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class ProductService(
    IProductPriceTableService productPriceTableService,
    IBarService barService,
    IUnitOfWork unitOfWork) : IProductService
{
    
    public async Task CreateProductAsync(Product product, CancellationToken cancellationToken)
    {
        var productDb = await unitOfWork.Products.GetByExpressionAsync(
            x => x.Name.ToLower() == product.Name.ToLower(), cancellationToken);
        if (productDb is not null)
            throw new ApplicationException("Já existe um produto com esse nome");
    }

    public async Task<Product> UpdateProductAsync(Guid id, Product product, CancellationToken cancellationToken)
    {
        var productDb = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (productDb is null)
            throw new EntityNotFoundException(id);

        productDb.SetPrice(product.Price);
        productDb.SetName(product.Name);
        productDb.SetLaboratoryId(product.LaboratoryId);
        productDb.Bars?.Clear();
        productDb.ProductPriceTables?.Clear();
        await UpdateBarsAsync(productDb.Id, product, cancellationToken);
        await UpdatePriceTableAsync(productDb.Id, product, cancellationToken);
        return productDb;
    }

    public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            throw new EntityNotFoundException(id);
        var bars = await barService.GetByProductId(product.Id, cancellationToken);
        foreach (var bar in bars)
            product.AddBar(bar);
        var tables = await productPriceTableService.GetByProductId(product.Id, cancellationToken);
        foreach (var table in tables)
            product.AddProductPriceTable(table);
        
        return product;
        
    }

    private async Task UpdateBarsAsync(Guid productId, Product product, CancellationToken cancellationToken)
    {
        await barService.RemoveRangeAsync(productId, cancellationToken);
        if (product.Bars != null)
        {
            var barsToInsert = product.Bars.Select(bar => new Bar(productId, bar.Code)).ToList();
            await barService.AddRangeAsync(barsToInsert, cancellationToken);
        }
    }

    private async Task UpdatePriceTableAsync(Guid productId, Product product, CancellationToken cancellationToken)
    {
        await productPriceTableService.RemoveRangeAsync(productId, cancellationToken);
        if (product.ProductPriceTables != null)
        {
            var tablesToInsert = product.ProductPriceTables.Select(table =>
                new ProductPriceTable(productId, table.MinimalAmount, table.MaximumAmount, table.Price)).ToList();
            await productPriceTableService.AddRangeAsync(tablesToInsert, cancellationToken);
        }
    }
}