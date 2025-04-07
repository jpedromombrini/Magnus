using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SaleReceiptController(ISaleReceiptAppService saleReceiptAppService)
{
    [HttpGet("getbyfilter")]
    public async Task<IEnumerable<SaleReceiptResponse>> GetSaleReceiptsByFilterAsync(
        [FromQuery] GetSaleReceiptFilter filter,
        CancellationToken cancellationToken)
    {
        return await saleReceiptAppService.GetSaleReceiptsByFilterAsync(filter, cancellationToken);
    }

    [HttpGet("getbysaleid/{id:guid}")]
    public async Task<IEnumerable<SaleReceiptResponse>> ListSaleReceiptByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await saleReceiptAppService.ListSaleReceiptByIdAsync(id, cancellationToken);
    }
}