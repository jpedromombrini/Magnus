using System.Linq.Expressions;
using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface ITransferWarehouseRepository : IRepository<TransferWarehouse>
{
    void UpdateStatusAsync(TransferWarehouseItem item);
    Task<TransferWarehouseItem?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TransferWarehouseItem>> GetItemsByStatusAsync(Expression<Func<TransferWarehouseItem, bool>> predicate, CancellationToken cancellationToken);
}