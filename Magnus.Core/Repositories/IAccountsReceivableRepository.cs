using System.Linq.Expressions;
using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IAccountsReceivableRepository : IRepository<AccountsReceivable>
{
    void DeleteAccountsReceivableRange(IEnumerable<AccountsReceivable> accountsReceivables);

    Task<IEnumerable<AccountsReceivable>> GetAllByFilterAsync(Expression<Func<AccountsReceivable, bool>> predicate,
        Guid? userId, CancellationToken cancellationToken);
}