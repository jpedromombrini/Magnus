using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IProductPriceTableService
{
    Task AddRangeAsync(IEnumerable<ProductPriceTable> productPriceTables, CancellationToken cancellationToken);
    Task<IEnumerable<ProductPriceTable>> GetByProductId(Guid productId, CancellationToken cancellationToken);
    Task RemoveRangeAsync(Guid productId, CancellationToken cancellationToken);
}