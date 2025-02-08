using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Magnus.Core.Enumerators;
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
        DateTime initialDate,
        DateTime finalDate,
        Guid clientId,
        Guid userId,
        int document,
        string status,
        CancellationToken cancellationToken)
    {
        if (status.Equals("todos", StringComparison.CurrentCultureIgnoreCase))
        {
            return await saleAppService.GetSalesByFilterAsync(x =>
                    x.CreateAt.Date >= initialDate.Date &&
                    x.CreateAt.Date <= finalDate.Date &&
                    (clientId == Guid.Empty || x.ClientId == clientId) &&
                    (userId == Guid.Empty || x.UserId == userId) &&
                    (document == 0 || x.Document == document),
                cancellationToken);
        }
        var queryStatus = status switch
        {
            "Abertos" => SaleStatus.Open,
            "Faturados" => SaleStatus.Invoiced,
            _ => SaleStatus.Open
        };
        return await saleAppService.GetSalesByFilterAsync(x =>
                x.CreateAt.Date >= initialDate.Date &&
                x.CreateAt.Date <= finalDate.Date &&
                (clientId == Guid.Empty || x.ClientId == clientId) &&
                (userId == Guid.Empty || x.UserId == userId) &&
                (document == 0 || x.Document == document) &&
                (x.Status == queryStatus),
            cancellationToken);
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
}