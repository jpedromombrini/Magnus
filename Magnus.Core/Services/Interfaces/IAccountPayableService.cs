using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IAccountPayableService
{
    Task CreateAsync(AccountsPayable accountsPayable, CancellationToken cancellationToken);
}