using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ReportController(IReportAppService reportAppService) : ControllerBase
{
    [HttpGet("saleBySellerReport")]
    public async Task<IEnumerable<SaleBySellerResponse>> GetAccountsReceivablesByFilterAsync(
        [FromQuery] SaleBySaleReportFilter filter,
        CancellationToken cancellationToken)
    {
        return await reportAppService.SaleBySellerReport(filter.InitialDate, filter.FinalDate, cancellationToken);
    }
}