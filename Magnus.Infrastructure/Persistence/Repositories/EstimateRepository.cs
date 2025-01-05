using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;
public class EstimateRepository(MagnusContext context) : Repository<Estimate>(context), IEstimateRepository
{
    private readonly MagnusContext _context = context;
    public override async Task<Estimate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Estimates
            .Include(x => x.Items)
            .Include(x => x.User)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<Estimate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Estimates
            .AsNoTracking()
            .Include(x => x.Items)
            .Include(x => x.User)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Estimate>> GetAllByExpressionAsync(Expression<Func<Estimate, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Estimates
            .AsNoTracking()
            .Where(predicate)
            .Include(x => x.Items)
            .Include(x => x.User)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Estimate?> GetByExpressionAsync(Expression<Func<Estimate, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Estimates
            .Where(predicate)
            .Include(x => x.Items)
            .Include(x => x.User)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public void DeleteItensRange(IEnumerable<EstimateItem> items)
    {
        _context.EstimateItems.RemoveRange(items);
    }
}