using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;

namespace Magnus.Core.Repositories;

public interface IAuditProductRepository : IRepository<AuditProduct>
{
    Task AddRangeAsync(IEnumerable<AuditProduct> entities, CancellationToken cancellationToken);
    void RemoveRange(IEnumerable<AuditProduct> entities);
    Task<int> GetBalanceAsync(Guid productId, AuditProductType type, int warehouseId,  CancellationToken cancellationToken);
}