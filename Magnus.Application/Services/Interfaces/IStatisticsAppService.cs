using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface IStatisticsAppService
{
    Task<IEnumerable<SalesBySellerResponse>> GetSalesBySellerAsync(DateOnly initialDate, DateOnly finalDate, CancellationToken cancellationToken);
}