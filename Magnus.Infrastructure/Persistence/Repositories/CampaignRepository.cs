using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class CampaignRepository(MagnusContext context) : Repository<Campaign>(context), ICampaignRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<IEnumerable<Campaign>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Campaigns.AsNoTracking()
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Campaign>> GetAllByExpressionAsync(
        Expression<Func<Campaign, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Campaigns.AsNoTracking()
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Campaign?> GetByExpressionAsync(Expression<Func<Campaign, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Campaigns
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public void RemoveItems(IEnumerable<CampaignItem> campaignItems)
    {
        _context.CampaignItems.RemoveRange(campaignItems);
    }

    public async Task<bool> ExistsCampaignWithProductInPeriodAsync(Guid productId, DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken)
    {
        return await _context.Campaigns
            .Include(c => c.Items)
            .Where(c => c.InitialDate <= endDate && c.FinalDate >= startDate && c.Active)
            .AnyAsync(c => c.Items.Any(i => i.ProductId == productId));
    }

    public override async Task<Campaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Campaigns
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}