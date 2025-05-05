using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class StatisticsController(IStatisticsAppService statisticsAppService) : ControllerBase
{
    [HttpGet("salebyseller")]
    public async Task<IEnumerable<SalesBySellerResponse>> SaleBySeller([FromQuery] SalesBySellerFilter filter, CancellationToken cancellationToken)
    {
        return await statisticsAppService.GetSalesBySellerAsync(filter.InitialDate, filter.FinalDate, cancellationToken);
    }
}