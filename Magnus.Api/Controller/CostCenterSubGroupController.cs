using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CostCenterSubGroupController(
    ICostCenterSubGroupAppService costCenterSubGroupAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<CostCenterSubGroupResponse>> GetAllCostCenterSubGroupsAsync(
        CancellationToken cancellationToken)
    {
        return await costCenterSubGroupAppService.GetCostCenterSubGroupsAsync(cancellationToken);
    }

    [HttpGet("getbyname")]
    public async Task<IEnumerable<CostCenterSubGroupResponse>> GetCostCenterSubGroupsByFilterAsync([FromQuery] string name,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            return [];
        return await costCenterSubGroupAppService.GetCostCenterSubGroupsByFilterAsync(
            x => x.Name.ToLower().Contains(name.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<CostCenterSubGroupResponse> GetCostCenterSubGroupByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await costCenterSubGroupAppService.GetCostCenterSubGroupByIdAsync(id, cancellationToken);
    }
    [HttpGet("getbygroupid/{groupId:guid}")]
    public async Task<IEnumerable<CostCenterSubGroupResponse>> GetCostCenterGroupByIdAsync(Guid groupId, CancellationToken cancellationToken)
    {
        return await costCenterSubGroupAppService.GetCostCenterSubGroupsByFilterAsync(x => x.CostCenterGroupId == groupId, cancellationToken);
    }

    [HttpPost]
    public async Task AddCostCenterSubGroupAsync([FromBody] CreateCostCenterSubGroupRequest request,
        CancellationToken cancellationToken)
    {
        await costCenterSubGroupAppService.AddCostCenterSubGroupAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateCostCenterSubGroupAsync(Guid id, [FromBody] UpdateCostCenterSubGroupRequest request,
        CancellationToken cancellationToken)
    {
        await costCenterSubGroupAppService.UpdateCostCenterSubGroupAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteCostCenterSubGroupAsync(Guid id, CancellationToken cancellationToken)
    {
        await costCenterSubGroupAppService.DeleteCostCenterSubGroupAsync(id, cancellationToken);
    }
}