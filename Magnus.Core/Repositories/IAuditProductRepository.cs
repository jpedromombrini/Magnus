using System.Linq.Expressions;
using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IAuditProductRepository : IRepository<AuditProduct>
{
    Task AddRangeAsync(IEnumerable<AuditProduct> entities, CancellationToken cancellationToken);
    void RemoveRange(IEnumerable<AuditProduct> entities);
}