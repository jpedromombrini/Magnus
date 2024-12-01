using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class CostCenterController(
    ICostCenterAppService costCenterAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<CostCenterResponse>> GetAllCostCentersAsync(CancellationToken cancellationToken)
    {
        return await costCenterAppService.GetCostCentersAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<CostCenterResponse>> GetCostCentersByFilterAsync([FromQuery] string filter,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filter))
            return [];
        return await costCenterAppService.GetCostCentersByFilterAsync(x => x.Name.ToLower().Contains(filter.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<CostCenterResponse> GetCostCenterByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await costCenterAppService.GetCostCenterByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddCostCenterAsync([FromBody] CreateCostCenterRequest request, CancellationToken cancellationToken)
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