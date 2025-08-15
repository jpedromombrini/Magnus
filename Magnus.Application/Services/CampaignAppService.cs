using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class CampaignAppService(
    IUnitOfWork unitOfWork,
    ICampaignService campaignService) : ICampaignAppService
{
    public async Task AddCampaignAsync(CreateCampaignRequest request, CancellationToken cancellationToken)
    {
        var campaign = request.MapToEntity();
        await campaignService.CreateCampaign(campaign, cancellationToken);
        await unitOfWork.Campaigns.AddAsync(campaign, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCampaignAsync(Guid id, UpdateCampaignRequest request, CancellationToken cancellationToken)
    {
        var campaign = request.MapToEntity();
        await campaignService.UpdateCampaign(id, campaign, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CampaignResponse>> GetCampaignsAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Campaigns.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<CampaignResponse>> GetCampaignsByFilterAsync(
        GetCampaingnFilter filter,
        CancellationToken cancellationToken)
    {
        return (await unitOfWork.Campaigns.GetAllByExpressionAsync(
                x =>
                    x.InitialDate <= filter.FinalDate &&
                    x.FinalDate >= filter.InitialDate &&
                    (filter.Active == null || x.Active == filter.Active),
                cancellationToken))
            .MapToResponse();
    }

    public async Task<CampaignResponse> GetCampaignByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var campaign = await unitOfWork.Campaigns.GetByIdAsync(id, cancellationToken);
        if (campaign == null)
            throw new EntityNotFoundException("Nenhuma campanha encontrada com esse Id");
        return campaign.MapToResponse();
    }

    public async Task DeleteCampaignAsync(Guid id, CancellationToken cancellationToken)
    {
        var campaign = await unitOfWork.Campaigns.GetByIdAsync(id, cancellationToken);
        if (campaign == null)
            throw new EntityNotFoundException("Nenhuma campanha encontrada com esse Id");
        unitOfWork.Campaigns.Delete(campaign);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}