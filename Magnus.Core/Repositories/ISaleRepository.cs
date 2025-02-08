using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface ISaleRepository : IRepository<Sale>
{
    void DeleteItensRange(IEnumerable<SaleItem> items);
    void DeleteReceiptsRange(IEnumerable<SaleReceipt> receipts);
}