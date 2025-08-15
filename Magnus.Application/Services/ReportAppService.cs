using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class ReportAppService(IUnitOfWork unitOfWork) : IReportAppService
{
    public async Task<IEnumerable<SaleBySellerResponse>> SaleBySellerReport(DateOnly initialDate, DateOnly finalDate,
        CancellationToken cancellationToken)
    {
        List<SaleBySellerResponse> saleBySellerReport = [];
        var sellers = (await unitOfWork.Sellers.GetAllAsync(cancellationToken)).MapToResponse();
        foreach (var seller in sellers)
        {
            var sales = (await unitOfWork.Sales.GetAllByExpressionAsync(
                x => x.UserId == seller.User.Id &&
                     DateOnly.FromDateTime(x.CreateAt) >= initialDate &&
                     DateOnly.FromDateTime(x.CreateAt) <= finalDate,
                cancellationToken)).MapToResponse();
            saleBySellerReport.Add(new SaleBySellerResponse(seller, sales));
        }

        return saleBySellerReport;
    }
}