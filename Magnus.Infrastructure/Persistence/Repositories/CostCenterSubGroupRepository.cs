using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class CostCenterSubGroupRepository(MagnusContext context)
    : Repository<CostCenterSubGroup>(context), ICostCenterSubGroupRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<CostCenterSubGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.CostCenterSubGroups
            .Include(x => x.CostCenterGroup)
            .Include(x => x.CostCenters)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<CostCenterSubGroup>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.CostCenterSubGroups
            .Include(x => x.CostCenterGroup)
            .Include(x => x.CostCenters)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<CostCenterSubGroup>> GetAllByExpressionAsync(
        Expression<Func<CostCenterSubGroup, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.CostCenterSubGroups
            .Where(predicate)
            .Include(x => x.CostCenterGroup)
            .Include(x => x.CostCenters)
            .ToListAsync(cancellationToken);
    }

    public override async Task<CostCenterSubGroup?> GetByExpressionAsync(
        Expression<Func<CostCenterSubGroup, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.CostCenterSubGroups
                .Include(x => x.CostCenterGroup)
                .Include(x => x.CostCenters)
                .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}