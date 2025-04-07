using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class AuditProductRepository(MagnusContext context) : Repository<AuditProduct>(context), IAuditProductRepository
{
    private readonly MagnusContext _context = context;

    public async Task AddRangeAsync(IEnumerable<AuditProduct> entities, CancellationToken cancellationToken)
    {
        await _context.AuditProducts.AddRangeAsync(entities, cancellationToken);
    }

    public void RemoveRange(IEnumerable<AuditProduct> entities)
    {
        _context.AuditProducts.RemoveRange(entities);
    }

    public async Task<int> GetBalanceAsync(Guid productId, AuditProductType type, int warehouseId,
        CancellationToken cancellationToken)
    {
        return await _context.AuditProducts.Where(
            x => x.ProductId == productId &&
                 x.WarehouseId == warehouseId &&
                 x.Type == type).SumAsync(x => x.Amount, cancellationToken);
    }
}