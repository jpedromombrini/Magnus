using System.Linq.Expressions;
using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IInvoiceRepository : IRepository<Invoice>
{
    Task<Invoice?> GetByExpressionAsync(Expression<Func<Invoice, bool>> predicate, CancellationToken cancellationToken);
}