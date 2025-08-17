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
            .AsQueryable();

        if (userId.HasValue && userId.Value != Guid.Empty)
            query = from ar in query
                join sri in _context.SaleReceiptInstallments
                    on ar.SaleReceiptInstallmentId equals sri.Id into sriJoin
                from sri in sriJoin.DefaultIfEmpty()
                join sr in _context.SaleReceipts
                    on sri.SaleReceiptId equals sr.Id into srJoin
                from sr in srJoin.DefaultIfEmpty()
                where sr != null && sr.UserId == userId.Value
                select ar;

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
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}