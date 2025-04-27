using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ISaleReceiptService
{
    Task DeleteSaleReceiptAsync(SaleReceipt id, CancellationToken cancellationToken);
    Task AddRangeAsync(Sale sale, IEnumerable<SaleReceipt> saleReceipts, CancellationToken cancellationToken);
    Task RevomeFromSaleAsync(Guid saleId, CancellationToken cancellationToken);
    Task<IEnumerable<SaleReceipt>> GetSaleReceiptsAsync(Guid saleId, CancellationToken cancellationToken);
    Task<SaleReceiptInstallment?> GetSaleReceiptInstallmentByIdAsync(Guid id, CancellationToken cancellationToken);
    
}