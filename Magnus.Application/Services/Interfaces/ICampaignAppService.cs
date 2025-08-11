using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface ICampaignAppService
{
    Task AddCampaignAsync(CreateCampaignRequest request, CancellationToken cancellationToken);
    Task UpdateCampaignAsync(Guid id, UpdateCampaignRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<CampaignResponse>> GetCampaignsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<CampaignResponse>> GetCampaignsByFilterAsync(GetCampaingnFilter filter,
        CancellationToken cancellationToken);

    Task<CampaignResponse> GetCampaignByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteCampaignAsync(Guid id, CancellationToken cancellationToken);
}