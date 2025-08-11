using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class AccountsPayableRepository(MagnusContext context)
    : Repository<AccountsPayable>(context), IAccountsPayableRepository
{
    private readonly MagnusContext _context = context;

    public async Task AddRangeAsync(IEnumerable<AccountsPayable> entities, CancellationToken cancellationToken)
    {
        await _context.AccountsPayables.AddRangeAsync(entities, cancellationToken);
    }

    public void RemoveRange(IEnumerable<AccountsPayable> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.Payment != null)
            {
                var tracked = _context.ChangeTracker.Entries<Payment>()
                    .FirstOrDefault(e => e.Entity.Id == entity.Payment.Id);
                if (tracked != null)
                    context.Entry(tracked.Entity).State = EntityState.Detached;
            }

            context.Remove(entity);
        }
    }

    public override async Task<IEnumerable<AccountsPayable>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.AccountsPayables
            .AsNoTracking()
            .Include(x => x.Payment)
            .ToListAsync(cancellationToken);
    }

    public override async Task<AccountsPayable> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.AccountsPayables
            .Include(x => x.Payment)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<AccountsPayable>> GetAllByExpressionAsync(
        Expression<Func<AccountsPayable, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.AccountsPayables
            .AsNoTracking()
            .Include(x => x.Payment)
            .Where(predicate).ToListAsync(cancellationToken);
    }

    public override async Task<AccountsPayable?> GetByExpressionAsync(Expression<Func<AccountsPayable, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.AccountsPayables
            .Include(x => x.Payment)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}