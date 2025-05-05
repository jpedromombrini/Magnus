using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class StatisticsAppService(IUnitOfWork unitOfWork) : IStatisticsAppService
{
    public async Task<IEnumerable<SalesBySellerResponse>> GetSalesBySellerAsync(DateOnly initialDate,
        DateOnly finalDate, CancellationToken cancellationToken)
    {
        var sales = await unitOfWork.Sales.GetAllByExpressionAsync(
                x => x.CreateAt >= initialDate.ToDateTime(TimeOnly.MinValue) &&
                     x.CreateAt <= finalDate.ToDateTime(TimeOnly.MaxValue), cancellationToken);
        var userIds = sales.Select(v => v.UserId).Distinct().ToList();
        var sellers = await unitOfWork.Sellers.GetAllByExpressionAsync(x => x.UserId != null && userIds.Contains((Guid)x.UserId), cancellationToken);
        var result = sales
            .GroupBy(v => v.UserId)
            .Select(g =>
            {
                var seller = sellers.FirstOrDefault(u => u.UserId == g.Key);
                return new SalesBySellerResponse(
                    SellerName: seller?.Name ?? "Desconhecido",
                    TotalSale: g.Sum(x => x.Value),
                    TotalFinantialDiscount: g.Sum(x => x.FinantialDiscount)
                );
            })
            .ToList();
        return result;
    }
}