using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class CostCenterRepository(MagnusContext context) : Repository<CostCenter>(context), ICostCenterRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<IEnumerable<CostCenter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.CostCenters
            .Include(x => x.CostCenterSubGroup)
            .ThenInclude(x => x.CostCenterGroup)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<CostCenter>> GetAllByExpressionAsync(
        Expression<Func<CostCenter, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.CostCenters
            .Where(predicate)
            .Include(x => x.CostCenterSubGroup)
            .ThenInclude(x => x.CostCenterGroup)
            .ToListAsync(cancellationToken);
    }

    public override async Task<CostCenter?> GetByExpressionAsync(Expression<Func<CostCenter, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.CostCenters
            .Where(predicate)
            .Include(x => x.CostCenterSubGroup)
            .ThenInclude(x => x.CostCenterGroup)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<CostCenter>> GetAllByTypeGroupAsync(CostcenterGroupType costcenterGroupType,
        CancellationToken cancellationToken)
    {
        return await _context.CostCenters
            .Include(cc => cc.CostCenterSubGroup)
            .ThenInclude(sg => sg.CostCenterGroup)
            .Where(cc => cc.CostCenterSubGroup.CostCenterGroup.CostcenterGroupType == costcenterGroupType)
            .ToListAsync(cancellationToken);
    }

    public override async Task<CostCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.CostCenters
            .Include(x => x.CostCenterSubGroup)
            .ThenInclude(x => x.CostCenterGroup)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}