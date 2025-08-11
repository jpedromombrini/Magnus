using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface ICampaignRepository : IRepository<Campaign>
{
    void RemoveItems(IEnumerable<CampaignItem> campaignItems);

    Task<bool> ExistsCampaignWithProductInPeriodAsync(Guid productId, DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken);
}