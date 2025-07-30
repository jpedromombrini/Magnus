using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class AppConfigurationRepository(MagnusContext context)
    : Repository<AppConfiguration>(context), IAppConfigurationRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<AppConfiguration?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.AppConfigurations
            .Include(x => x.CostCenterSale)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<AppConfiguration>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.AppConfigurations
            .Include(x => x.CostCenterSale)
            .ThenInclude(x => x.CostCenterSubGroup)
            .ThenInclude(x => x.CostCenterGroup)
            .ToListAsync(cancellationToken);
    }
}