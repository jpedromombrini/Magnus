using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IAccountsPayableRepository : IRepository<AccountsPayable>
{
    Task AddRangeAsync(IEnumerable<AccountsPayable> entities, CancellationToken cancellationToken);
    void RemoveRange(IEnumerable<AccountsPayable> entities);
}