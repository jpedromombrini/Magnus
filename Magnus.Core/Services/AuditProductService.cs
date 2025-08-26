using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Helpers;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class AuditProductService(
    IUnitOfWork unitOfWork) : IAuditProductService
{
    public async Task SaleMovimentAsync(Sale sale, int warehouseCode, CancellationToken cancellationToken)
    {
        var audits = new List<AuditProduct>(sale.Items.Count);
        audits.AddRange(sale.Items.Select(item => new AuditProduct(item.ProductId, DateTimeHelper.NowInBrasilia(),
            sale.Document,
            item.Amount, item.TotalPrice, AuditProductType.Out, null, 999, warehouseCode, null, sale.ClientId,
            sale.Id)));
        await unitOfWork.AuditProducts.AddRangeAsync(audits, cancellationToken);
    }

    public async Task CancelSaleMovimentAsync(Sale sale, CancellationToken cancellationToken)
    {
        var audits =
            await unitOfWork.AuditProducts.GetAllByExpressionAsync(x => x.SaleId == sale.Id, cancellationToken);
        var auditProducts = audits.ToList();
        unitOfWork.AuditProducts.RemoveRange(auditProducts);
    }
}