using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IEstimateRepository : IRepository<Estimate>
{
    void DeleteItensRange(IEnumerable<EstimateItem> items);
    void DeleteReceiptRange(IEnumerable<EstimateReceipt> receipts);
    Task AddItensRangeAsync(IEnumerable<EstimateItem> items, CancellationToken cancellationToken);
}