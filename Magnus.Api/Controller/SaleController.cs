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
public class SaleController(
    ISaleAppService saleAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<SaleResponse>> GetAllsSalesAsync(CancellationToken cancellationToken)
    {
        return await saleAppService.GetSalesAsync(cancellationToken);
    }

    [HttpGet("getbyfilter")]
    public async Task<IEnumerable<SaleResponse>> GetSalesByFilterAsync(
        [FromQuery] GetSaleFilter filter,
        CancellationToken cancellationToken)
    {
        return await saleAppService.GetSalesByFilterAsync(filter, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<SaleResponse> GetSaleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await saleAppService.GetSaleByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddSaleAsync([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        await saleAppService.AddSaleAsync(request, cancellationToken);
    }

    [HttpPost("invoice")]
    public async Task InvoiceSaleAsync([FromBody] InvoiceSaleRequest request, CancellationToken cancellationToken)
    {
        await saleAppService.InvoiceSaleAsync(request.Id, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateSaleAsync(Guid id, [FromBody] UpdateSaleRequest request,
        CancellationToken cancellationToken)
    {
        await saleAppService.UpdateSaleAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteSaleAsync(Guid id, CancellationToken cancellationToken)
    {
        await saleAppService.DeleteSaleAsync(id, cancellationToken);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task CancelSaleAsync(Guid id, [FromBody] SaleCancelReasonRequest request,
        CancellationToken cancellationToken)
    {
        await saleAppService.CancelSaleAsync(id, request, cancellationToken);
    }

    [HttpGet("generateCr")]
    public async Task GenerateCrAsync(CancellationToken cancellationToken)
    {
        await saleAppService.GenerateAccontsReceivable(cancellationToken);
    }
}