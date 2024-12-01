using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class CostCenterGroupController(
    ICostCenterGroupAppService costCenterGroupAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<CostCenterGroupResponse>> GetAllCostCenterGroupsAsync(
        CancellationToken cancellationToken)
    {
        return await costCenterGroupAppService.GetCostCenterGroupsAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<CostCenterGroupResponse>> GetCostCenterGroupsByFilterAsync([FromQuery] string filter,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filter))
            return [];
        return await costCenterGroupAppService.GetCostCenterGroupsByFilterAsync(
            x => x.Name.ToLower().Contains(filter.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<CostCenterGroupResponse> GetCostCenterGroupByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await costCenterGroupAppService.GetCostCenterGroupByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddCostCenterGroupAsync([FromBody] CreateCostCenterGroupRequest request,
        CancellationToken cancellationToken)
    {
        await costCenterGroupAppService.AddCostCenterGroupAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateCostCenterGroupAsync(Guid id, [FromBody] UpdateCostCenterGroupRequest request,
        CancellationToken cancellationToken)
    {
        await costCenterGroupAppService.UpdateCostCenterGroupAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteCostCenterGroupAsync(Guid id, CancellationToken cancellationToken)
    {
        await costCenterGroupAppService.DeleteCostCenterGroupAsync(id, cancellationToken);
    }
}