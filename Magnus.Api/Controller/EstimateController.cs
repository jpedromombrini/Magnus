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
public class EstimateController(
    IEstimateAppService estimateAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<EstimateResponse>> GetAllEstimatesAsync(CancellationToken cancellationToken)
    {
        return await estimateAppService.GetEstimatesAsync(cancellationToken);
    }

    [HttpGet("getbyfilter")]
    public async Task<IEnumerable<EstimateResponse>> GetEstimatesByFilterAsync([FromQuery] GetEstimatesFilter filter,
        CancellationToken cancellationToken)
    {
        return await estimateAppService.GetEstimatesByFilterAsync(filter, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<EstimateResponse> GetEstimateByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await estimateAppService.GetEstimateByIdAsync(id, cancellationToken);
    }

    [HttpGet("getreport/{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateReportAsync(Guid id, CancellationToken cancellationToken)
    {
        return File(await estimateAppService.CreatePdf(id, cancellationToken), "application/pdf", $"{id}.pdf");
    }

    [HttpPost]
    public async Task AddEstimateAsync([FromBody] CreateEstimateRequest request, CancellationToken cancellationToken)
    {
        await estimateAppService.AddEstimateAsync(request, cancellationToken);
    }

    [HttpPost("createsale/{id:guid}")]
    public async Task CreateSaleAsync(Guid id, CancellationToken cancellationToken)
    {
        await estimateAppService.CreateSaleAsync(id, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateEstimateAsync(Guid id, [FromBody] UpdateEstimateRequest request,
        CancellationToken cancellationToken)
    {
        await estimateAppService.UpdateEstimateAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteEstimateAsync(Guid id, CancellationToken cancellationToken)
    {
        await estimateAppService.DeleteEstimateAsync(id, cancellationToken);
    }
}