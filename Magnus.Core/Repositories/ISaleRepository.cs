using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface ISaleRepository : IRepository<Sale>
{
    void DeleteItensRange(IEnumerable<SaleItem> items);
    void DeleteReceiptsRange(IEnumerable<SaleReceipt> receipts);
    Task AddItemsRangeAsync(IEnumerable<SaleItem> items, CancellationToken cancellationToken);
    Task AddReceiptsRangeAsync(IEnumerable<SaleReceipt> receipts, CancellationToken cancellationToken);
}