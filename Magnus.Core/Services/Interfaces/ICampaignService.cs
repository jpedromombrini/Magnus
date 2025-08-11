using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ICampaignService
{
    Task CreateCampaign(Campaign campaign, CancellationToken cancellationToken);
    Task UpdateCampaign(Guid id, Campaign campaign, CancellationToken cancellationToken);
}