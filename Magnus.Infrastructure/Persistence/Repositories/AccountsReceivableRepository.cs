using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class AccountsReceivableRepository(MagnusContext context) : Repository<AccountsReceivable>(context), IAccountsReceivableRepository
{
    private readonly MagnusContext _context = context;
    public void DeleteAccountsReceivableRange(IEnumerable<AccountsReceivable> accountsReceivables)
    {
        _context.AccountsReceivables.RemoveRange(accountsReceivables);
    }
}