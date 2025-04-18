using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class ProductPriceTableService(IUnitOfWork unitOfWork) : IProductPriceTableService
{
    public async Task AddRangeAsync(IEnumerable<ProductPriceTable> productPriceTables,
        CancellationToken cancellationToken)
    {
        await unitOfWork.ProductPriceTables.AddRangeAsync(productPriceTables, cancellationToken);
    }

    public async Task<IEnumerable<ProductPriceTable>> GetByProductId(Guid productId,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.ProductPriceTables.GetAllByExpressionAsync(x => x.ProductId == productId,
            cancellationToken);
    }

    public async Task RemoveRangeAsync(Guid productId, CancellationToken cancellationToken)
    {
        var productsTable =  await GetByProductId(productId, cancellationToken);
        unitOfWork.ProductPriceTables.RemoveRange(productsTable);
    }
}