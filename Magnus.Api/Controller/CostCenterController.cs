using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Enumerators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CostCenterController(
    ICostCenterAppService costCenterAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<CostCenterResponse>> GetAllCostCentersAsync(CancellationToken cancellationToken)
    {
        return await costCenterAppService.GetCostCentersAsync(cancellationToken);
    }

    [HttpGet("getbytype")]
    public async Task<IEnumerable<CostCenterResponse>> GetAllByTypeCostCentersAsync(
        [FromQuery] CostcenterGroupType costcenterGroupType, CancellationToken cancellationToken)
    {
        return await costCenterAppService.GetCostCentersByTypeAsync(costcenterGroupType, cancellationToken);
    }

    [HttpGet("GetByName")]
    public async Task<IEnumerable<CostCenterResponse>> GetCostCentersByFilterAsync([FromQuery] string name,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            return [];
        return await costCenterAppService.GetCostCentersByFilterAsync(x => x.Name.ToLower().Contains(name.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<CostCenterResponse> GetCostCenterByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await costCenterAppService.GetCostCenterByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddCostCenterAsync([FromBody] CreateCostCenterRequest request,
        CancellationToken cancellationToken)
    {
        await costCenterAppService.AddCostCenterAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateCostCenterAsync(Guid id, [FromBody] UpdateCostCenterRequest request,
        CancellationToken cancellationToken)
    {
        await costCenterAppService.UpdateCostCenterAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteCostCenterAsync(Guid id, CancellationToken cancellationToken)
    {
        await costCenterAppService.DeleteCostCenterAsync(id, cancellationToken);
    }
}