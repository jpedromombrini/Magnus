using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class StockMovementRepository(MagnusContext context)
    : Repository<StockMovement>(context), IStockMovementRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<StockMovement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.StockMovements
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<StockMovement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.StockMovements
            .Include(x => x.Product)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<StockMovement>> GetAllByExpressionAsync(
        Expression<Func<StockMovement, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.StockMovements
            .Include(x => x.Product)
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public override async Task<StockMovement?> GetByExpressionAsync(Expression<Func<StockMovement, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.StockMovements
            .Include(x => x.Product)
            .Where(predicate)
            .FirstOrDefaultAsync(cancellationToken);
    }
}