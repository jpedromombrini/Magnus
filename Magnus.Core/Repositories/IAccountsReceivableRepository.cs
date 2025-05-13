using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IAccountsReceivableRepository : IRepository<AccountsReceivable>
{
    void DeleteAccountsReceivableRange(IEnumerable<AccountsReceivable> accountsReceivables);
}