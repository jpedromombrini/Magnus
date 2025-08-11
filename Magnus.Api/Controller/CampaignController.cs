using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CampaignController(ICampaignAppService campaignAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<CampaignResponse>> GetAllCampaignsAsync(CancellationToken cancellationToken)
    {
        return await campaignAppService.GetCampaignsAsync(cancellationToken);
    }

    [HttpGet("getbyfilter")]
    public async Task<IEnumerable<CampaignResponse>> GetCampaignsByFilterAsync([FromQuery] GetCampaingnFilter filter,
        CancellationToken cancellationToken)
    {
        return await campaignAppService.GetCampaignsByFilterAsync(filter, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<CampaignResponse> GetCampaignByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await campaignAppService.GetCampaignByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddCampaignAsync([FromBody] CreateCampaignRequest request, CancellationToken cancellationToken)
    {
        await campaignAppService.AddCampaignAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateCampaignAsync(Guid id, [FromBody] UpdateCampaignRequest request,
        CancellationToken cancellationToken)
    {
        await campaignAppService.UpdateCampaignAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteCampaignAsync(Guid id, CancellationToken cancellationToken)
    {
        await campaignAppService.DeleteCampaignAsync(id, cancellationToken);
    }
}