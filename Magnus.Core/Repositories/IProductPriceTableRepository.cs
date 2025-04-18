using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IProductPriceTableRepository : IRepository<ProductPriceTable>
{
    Task AddRangeAsync(IEnumerable<ProductPriceTable> productPriceTables, CancellationToken cancellationToken);
    void RemoveRange(IEnumerable<ProductPriceTable> productPriceTables);
    
}