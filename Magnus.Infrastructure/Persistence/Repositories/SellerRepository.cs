using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class SellerRepository(MagnusContext context) : Repository<Seller>(context), ISellerRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<Seller?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Sellers.Include(x => x.User)
            .ThenInclude(x => x.Warehouse)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<Seller>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Sellers.AsNoTracking().Include(x => x.User)
            .ThenInclude(x => x.Warehouse)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Seller>> GetAllByExpressionAsync(Expression<Func<Seller, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Sellers.AsNoTracking().Include(x => x.User)
            .ThenInclude(x => x.Warehouse)
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Seller?> GetByExpressionAsync(Expression<Func<Seller, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Sellers.Include(x => x.User)
            .ThenInclude(x => x.Warehouse)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}