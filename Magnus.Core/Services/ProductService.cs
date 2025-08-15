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
            throw new BusinessRuleException("Já existe um produto com esse nome");
        if (product.ProductGroupId != null && product.ProductGroupId != Guid.Empty)
        {
            var productGroup =
                await unitOfWork.ProductGroups.GetByIdAsync((Guid)product.ProductGroupId, cancellationToken);
            if (productGroup != null) productDb?.SetProductGroup(productGroup);
        }
    }

    public async Task<Product> UpdateProductAsync(Guid id, Product product, CancellationToken cancellationToken)
    {
        var productDb = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (productDb is null)
            throw new EntityNotFoundException(id);

        productDb.SetPrice(product.Price);
        productDb.SetName(product.Name);
        productDb.SetLaboratoryId(product.LaboratoryId);
        productDb.SetApplyPriceRule(product.ApplyPriceRule);
        productDb.SetProductGroupId(product.ProductGroupId);
        if (product.ProductGroupId != null && product.ProductGroupId != Guid.Empty)
        {
            var productGroup =
                await unitOfWork.ProductGroups.GetByIdAsync((Guid)product.ProductGroupId, cancellationToken);
            if (productGroup != null) productDb?.SetProductGroup(productGroup);
        }

        UpdateBarsAsync(product, productDb);
        UpdatePriceTableAsync(product, productDb);
        return productDb;
    }

    public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            throw new EntityNotFoundException(id);
        return product;
    }

    private void UpdateBarsAsync(Product product, Product productDb)
    {
        if (productDb.Bars is not null)
        {
            unitOfWork.Bars.RemoveRange(productDb.Bars);
            productDb.Bars?.Clear();
        }

        if (product.Bars is null) return;
        foreach (var barRequest in product.Bars)
        {
            var bar = new Bar(barRequest.Code);
            bar.SetProductId(productDb.Id);
            productDb.AddBar(bar);
        }
    }

    private void UpdatePriceTableAsync(Product product, Product productDb)
    {
        if (productDb.ProductPriceTables is not null)
        {
            unitOfWork.ProductPriceTables.RemoveRange(productDb.ProductPriceTables);
            productDb.ProductPriceTables?.Clear();
        }

        if (product.ProductPriceTables is null) return;
        foreach (var priceTableRequest in product.ProductPriceTables)
        {
            var priceTable = new ProductPriceTable(priceTableRequest.MinimalAmount, priceTableRequest.MaximumAmount,
                priceTableRequest.Price);
            priceTable.SetProductId(productDb.Id);
            productDb.AddProductPriceTable(priceTable);
        }
    }
}