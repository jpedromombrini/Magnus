using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IAuditProductService
{
    Task SaleMovimentAsync(Sale sale, int warehouseCode, CancellationToken cancellationToken);
    Task CancelSaleMovimentAsync(Sale sale, CancellationToken cancellationToken);
}