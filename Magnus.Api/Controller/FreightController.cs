using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;
[ApiController]
[Route("[controller]")]
[Authorize]
public class FreightController(IFreightAppService freightAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<FreightResponse>> GetAllFreightsAsync(CancellationToken cancellationToken)
    {
        return await freightAppService.GetFreightsAsync(cancellationToken);
    }

    [HttpGet("GetByName")]
    public async Task<IEnumerable<FreightResponse>> GetFreightsByFilterAsync(
        string name,
        CancellationToken cancellationToken)
    {
        return await freightAppService.GetFreightsByFilterAsync(x => x.Name.ToLower() == name.ToLower(),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<FreightResponse> GetFreightByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await freightAppService.GetFreightByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddFreightAsync([FromBody] CreateFreightRequest request, CancellationToken cancellationToken)
    {
        await freightAppService.AddFreightAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateFreightAsync(Guid id, [FromBody] UpdateFreightRequest request,
        CancellationToken cancellationToken)
    {
        await freightAppService.UpdateFreightAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteFreightAsync(Guid id, CancellationToken cancellationToken)
    {
        await freightAppService.DeleteFreightAsync(id, cancellationToken);
    }
}