using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ISaleReceiptService
{
    Task AddRangeAsync(Sale sale, IEnumerable<SaleReceipt> saleReceipts, CancellationToken cancellationToken);
    void RevomeFromSaleAsync(IEnumerable<SaleReceipt> receipts, CancellationToken cancellationToken);
    Task<IEnumerable<SaleReceipt>> GetSaleReceiptsAsync(Guid saleId, CancellationToken cancellationToken);
    Task<SaleReceiptInstallment?> GetSaleReceiptInstallmentByIdAsync(Guid id, CancellationToken cancellationToken);
    
}