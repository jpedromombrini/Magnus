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
    public async Task<IEnumerable<SaleBySellerResponse>> SaleBySellerReportAsync(
        [FromQuery] SalesBySellerFilter filter,
        CancellationToken cancellationToken)
    {
        return await reportAppService.SaleBySellerReport(filter.InitialDate, filter.FinalDate, cancellationToken);
    }

    [HttpGet("saleByProductReport")]
    public async Task<IEnumerable<SaleByProductResponse>> SaleByProductReportAsync(
        [FromQuery] SalesByProductFilter filter,
        CancellationToken cancellationToken)
    {
        return await reportAppService.SaleByProductReport(filter.InitialDate, filter.FinalDate, cancellationToken);
    }

    [HttpGet("saleByGroupReport")]
    public async Task<IEnumerable<SaleByGroupResponse>> SaleByGroupReportAsync(
        [FromQuery] SalesByGroupFilter filter,
        CancellationToken cancellationToken)
    {
        return await reportAppService.SaleByGroupReport(filter.InitialDate, filter.FinalDate, cancellationToken);
    }
}