using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface IReportAppService
{
    Task<IEnumerable<SaleBySellerResponse>> SaleBySellerReport(DateOnly initialDate, DateOnly finalDate,
        CancellationToken cancellationToken);
}