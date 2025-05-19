using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IAccountsPayableService
{
    Task CreateAsync(AccountsPayable accountsPayable, CancellationToken cancellationToken);
}