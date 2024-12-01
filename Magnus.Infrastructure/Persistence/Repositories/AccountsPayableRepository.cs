using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class AccountsPayableRepository(MagnusContext context) : Repository<AccountsPayable>(context), IAccountsPayableRepository
{
    private readonly MagnusContext _context = context;
    public async Task AddRangeAsync(IEnumerable<AccountsPayable> entities, CancellationToken cancellationToken)
    {
        await _context.AccountsPayables.AddRangeAsync(entities, cancellationToken);
    }

    public void RemoveRange(IEnumerable<AccountsPayable> entities)
    {
        _context.AccountsPayables.RemoveRange(entities);   
    }
}