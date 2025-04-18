using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class ProductPriceTableRepository(MagnusContext context) : Repository<ProductPriceTable>(context), IProductPriceTableRepository
{
    private readonly MagnusContext _context = context;
    public async Task AddRangeAsync(IEnumerable<ProductPriceTable> productPriceTables, CancellationToken cancellationToken)
    {
        await _context.ProductPriceTables.AddRangeAsync(productPriceTables, cancellationToken);
    }

    public void RemoveRange(IEnumerable<ProductPriceTable> productPriceTables)
    {
        _context.ProductPriceTables.RemoveRange(productPriceTables);
    }
}