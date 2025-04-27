using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class CostCenterGroupRepository(MagnusContext context)
    : Repository<CostCenterGroup>(context), ICostCenterGroupRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<IEnumerable<CostCenterGroup>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.CostCenterGroups
            .Include(x => x.CostCenterSubGroups)
            .ThenInclude(x => x.CostCenters)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<CostCenterGroup>> GetAllByExpressionAsync(Expression<Func<CostCenterGroup, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.CostCenterGroups
            .Where(predicate)
            .Include(x => x.CostCenterSubGroups)
            .ThenInclude(x => x.CostCenters)
            .ToListAsync(cancellationToken);
    }

    public override async Task<CostCenterGroup?> GetByExpressionAsync(Expression<Func<CostCenterGroup, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.CostCenterGroups
            .Where(predicate)
            .Include(x => x.CostCenterSubGroups)
            .ThenInclude(x => x.CostCenters)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public override async Task<CostCenterGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.CostCenterGroups
                .Include(x => x.CostCenterSubGroups)
                .ThenInclude(x => x.CostCenters)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}