using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IAccountPayableService
{
    Task CreateAsync(List<AccountsPayable> accountsPayables, CancellationToken cancellationToken);

    Task UpdateAsync(AccountsPayable accountsPayableDb, AccountsPayable accountsPayable,
        CancellationToken cancellationToken);
}