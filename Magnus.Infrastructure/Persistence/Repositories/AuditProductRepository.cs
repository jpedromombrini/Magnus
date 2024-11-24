using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;

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
}