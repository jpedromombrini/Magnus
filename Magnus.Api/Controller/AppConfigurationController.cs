using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
public class AppConfigurationController(
    IAppConfigurationAppService appConfigurationAppService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<AppConfigurationResponse> GetAppConfigurationAsync(CancellationToken cancellationToken)
    {
        return await appConfigurationAppService.GetAppConfigurationAsync(cancellationToken);
    }
    [HttpPost]
    public async Task AddAppConfigurationAsync([FromBody] CreateAppConfigurationRequest request, 
        CancellationToken cancellationToken)
    {
        await appConfigurationAppService.AddAppConfigurationAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateAppConfigurationAsync(Guid id, [FromBody] UpdateAppConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        await appConfigurationAppService.UpdateAppConfigurationAsync(id, request, cancellationToken);
    }
}