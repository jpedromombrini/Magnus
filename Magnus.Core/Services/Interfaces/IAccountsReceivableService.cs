using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IAccountsReceivableService
{
    Task CreateAsync(AccountsReceivable accountsReceivable, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, AccountsReceivable accountsReceivable, CancellationToken cancellationToken);
    Task<AccountsReceivable?> GetBySaleReceiptInstallmentIdAsync(Guid saleReceiptInstallmentId, CancellationToken cancellationToken);
    void RemoveRangeAsync(IEnumerable<AccountsReceivable> accountsReceivables);
}