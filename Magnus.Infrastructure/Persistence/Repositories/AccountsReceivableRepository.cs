using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class AccountsReceivableRepository(MagnusContext context)
    : Repository<AccountsReceivable>(context), IAccountsReceivableRepository
{
    private readonly MagnusContext _context = context;

    public void DeleteAccountsReceivableRange(IEnumerable<AccountsReceivable> accountsReceivables)
    {
        _context.AccountsReceivables.RemoveRange(accountsReceivables);
    }

    public async Task<IEnumerable<AccountsReceivable>> GetAllByFilterAsync(
        Expression<Func<AccountsReceivable, bool>> predicate,
        Guid? userId, CancellationToken cancellationToken)
    {
        var query = _context.AccountsReceivables
            .Where(predicate)
            .Include(x => x.Client)
            .ThenInclude(c => c.Address)
            .Include(x => x.Client)
            .ThenInclude(c => c.Phones)
            .Include(x => x.Client)
            .ThenInclude(c => c.SocialMedias)
            .Include(x => x.Receipt)
            .Include(x => x.AccountsReceivableOccurences)
            .ThenInclude(x => x.User)
            .AsQueryable();

        if (userId.HasValue && userId.Value != Guid.Empty)
            query = query.Where(ar => _context.SaleReceipts
                .Where(sr => sr.UserId == userId.Value)
                .Any(sr => _context.SaleReceiptInstallments
                    .Where(sri => sri.SaleReceiptId == sr.Id)
                    .Any(sri => sri.Id == ar.SaleReceiptInstallmentId)));

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public override async Task<AccountsReceivable?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.AccountsReceivables
            .Include(x => x.Client)
            .ThenInclude(c => c.Address)
            .Include(x => x.Client)
            .ThenInclude(c => c.Phones)
            .Include(x => x.Client)
            .ThenInclude(c => c.SocialMedias)
            .Include(x => x.CostCenter)
            .ThenInclude(x => x.CostCenterSubGroup)
            .ThenInclude(x => x.CostCenterGroup)
            .Include(x => x.Receipt)
            .Include(x => x.AccountsReceivableOccurences)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<AccountsReceivable>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.AccountsReceivables
            .AsNoTracking()
            .Include(x => x.Client)
            .ThenInclude(c => c.Address)
            .Include(x => x.Client)
            .ThenInclude(c => c.Phones)
            .Include(x => x.Client)
            .ThenInclude(c => c.SocialMedias)
            .Include(x => x.CostCenter)
            .ThenInclude(x => x.CostCenterSubGroup)
            .ThenInclude(x => x.CostCenterGroup)
            .Include(x => x.Receipt)
            .Include(x => x.AccountsReceivableOccurences)
            .ThenInclude(x => x.User)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<AccountsReceivable>> GetAllByExpressionAsync(
        Expression<Func<AccountsReceivable, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.AccountsReceivables
            .AsNoTracking()
            .Include(x => x.Client)
            .ThenInclude(c => c.Address)
            .Include(x => x.Client)
            .ThenInclude(c => c.Phones)
            .Include(x => x.Client)
            .ThenInclude(c => c.SocialMedias)
            .Include(x => x.CostCenter)
            .ThenInclude(x => x.CostCenterSubGroup)
            .ThenInclude(x => x.CostCenterGroup)
            .Include(x => x.Receipt)
            .Include(x => x.AccountsReceivableOccurences)
            .ThenInclude(x => x.User)
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public override async Task<AccountsReceivable?> GetByExpressionAsync(
        Expression<Func<AccountsReceivable, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.AccountsReceivables
            .Include(x => x.Client)
            .ThenInclude(c => c.Address)
            .Include(x => x.Client)
            .ThenInclude(c => c.Phones)
            .Include(x => x.Client)
            .ThenInclude(c => c.SocialMedias)
            .Include(x => x.CostCenter)
            .ThenInclude(x => x.CostCenterSubGroup)
            .ThenInclude(x => x.CostCenterGroup)
            .Include(x => x.Receipt)
            .Include(x => x.AccountsReceivableOccurences)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}