using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IAccountsReceivableService
{
    Task CreateAsync(AccountsReceivable accountsReceivable, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, AccountsReceivable accountsReceivable, CancellationToken cancellationToken);
}