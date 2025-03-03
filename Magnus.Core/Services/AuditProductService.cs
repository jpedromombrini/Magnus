using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class AuditProductService(
    IUnitOfWork unitOfWork) : IAuditProductService
{
    public async Task SaleMovimentAsync(Sale sale, int warehouseCode, CancellationToken cancellationToken)
    {
        var audits = new List<AuditProduct>(sale.Items.Count);
        audits.AddRange(sale.Items.Select(item => new AuditProduct(item.ProductId, DateTime.Now, sale.Document,
            item.Amount, item.TotalPrice, AuditProductType.Out, null, 999, warehouseCode, null, sale.ClientId,
            sale.Id)));
        await unitOfWork.AuditProducts.AddRangeAsync(audits, cancellationToken);
    }
}