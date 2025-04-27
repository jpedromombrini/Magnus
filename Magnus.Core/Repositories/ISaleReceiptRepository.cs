using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface ISaleReceiptRepository : IRepository<SaleReceipt>
{
    Task AddRangeAsync(IEnumerable<SaleReceipt> saleReceipts, CancellationToken cancellationToken);
    void RemoveRange(IEnumerable<SaleReceipt> saleReceipts);
    Task<SaleReceiptInstallment?> GetSaleReceiptInstallmentByIdAsync(Guid id, CancellationToken cancellationToken);
}