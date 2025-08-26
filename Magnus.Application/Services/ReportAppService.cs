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

    public async Task<IEnumerable<SaleByProductResponse>> SaleByProductReport(DateOnly initialDate, DateOnly finalDate,
        CancellationToken cancellationToken)
    {
        var start = initialDate.ToDateTime(TimeOnly.MinValue);
        var end = finalDate.ToDateTime(TimeOnly.MaxValue);

        var products = (await unitOfWork.Products.GetAllAsync(cancellationToken))
            .MapToResponse()
            .ToList();

        var users = (await unitOfWork.Users.GetAllAsync(cancellationToken))
            .ToDictionary(u => u.Id, u => u.Name);

        var sales = await unitOfWork.Sales.GetAllByExpressionAsync(
            x => x.CreateAt >= start && x.CreateAt <= end,
            cancellationToken
        );
        var itemsWithSeller = sales
            .SelectMany(s => s.Items.Select(i => new
            {
                s.UserId,
                SellerName = users.TryGetValue(s.UserId, out var name) ? name : "Desconhecido",
                i.ProductId,
                i.Amount,
                i.TotalPrice,
                i.Discount
            }));

        var grouped = itemsWithSeller
            .GroupBy(x => new { x.ProductId, x.UserId, x.SellerName })
            .Select(g => new
            {
                g.Key.ProductId,
                g.Key.UserId,
                g.Key.SellerName,
                Amount = g.Sum(i => i.Amount),
                TotalSale = g.Sum(i => i.TotalPrice - i.Discount)
            })
            .ToList();
        var groupedByProduct = grouped
            .GroupBy(g => g.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => new SaleByProductSellerResponse(
                    x.UserId,
                    x.SellerName,
                    x.Amount,
                    x.TotalSale
                ))
            );

        var result = products
            .Select(p =>
            {
                var salesBySeller = groupedByProduct.TryGetValue(p.Id, out var list)
                    ? list
                    : Enumerable.Empty<SaleByProductSellerResponse>();

                return new SaleByProductResponse(
                    p,
                    salesBySeller.Sum(x => x.Amount),
                    salesBySeller.Sum(x => x.TotalSale),
                    salesBySeller
                );
            })
            .ToList();

        return result;
    }

    public async Task<IEnumerable<SaleByGroupResponse>> SaleByGroupReport(DateOnly initialDate, DateOnly finalDate,
        CancellationToken cancellationToken)
    {
        var start = initialDate.ToDateTime(TimeOnly.MinValue);
        var end = finalDate.ToDateTime(TimeOnly.MaxValue);

        var sales = await unitOfWork.Sales.GetAllByExpressionAsync(
            s => s.CreateAt >= start && s.CreateAt <= end,
            cancellationToken
        );

        var items = sales
            .SelectMany(s => s.Items ?? [])
            .ToList();

        if (items.Count == 0)
            return [];

        var productIds = items.Select(x => x.ProductId).Distinct().ToList();

        var products = await unitOfWork.Products.GetAllByExpressionAsync(
            p => productIds.Contains(p.Id),
            cancellationToken
        );

        var productDict = products.ToDictionary(
            p => p.Id,
            p => new
            {
                ProductName = p.Name,
                GroupName = p.ProductGroup?.Name ?? "Sem Grupo"
            }
        );

        var enriched = items.Select(x =>
        {
            if (!productDict.TryGetValue(x.ProductId, out var pd))
                pd = new { ProductName = "(Produto não encontrado)", GroupName = "Sem Grupo" };

            return new
            {
                pd.GroupName,
                pd.ProductName,
                x.Amount,
                Net = x.TotalPrice - x.Discount
            };
        });

        var result = enriched
            .GroupBy(x => x.GroupName)
            .Select(g => new SaleByGroupResponse(
                g.Key,
                g.Sum(x => x.Amount),
                g.Sum(x => x.Net),
                g.GroupBy(x => x.ProductName)
                    .Select(pg => new SaleProductByGroup(
                        pg.Key,
                        pg.Sum(x => x.Amount),
                        pg.Sum(x => x.Net)
                    ))
                    .OrderByDescending(p => p.TotalValue)
            ))
            .OrderByDescending(r => r.TotalValue)
            .ToList();

        return result;
    }
}