using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class ProductGroupRepository(MagnusContext context) : Repository<ProductGroup>(context), IProductGroupRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<IEnumerable<ProductGroup>> GetAllByExpressionAsync(
        Expression<Func<ProductGroup, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.ProductGroups.AsNoTracking()
            .Where(predicate)
            .Include(x => x.Products)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<ProductGroup>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ProductGroups.AsNoTracking()
            .Include(x => x.Products)
            .ToListAsync(cancellationToken);
    }

    public override async Task<ProductGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ProductGroups.Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<ProductGroup?> GetByExpressionAsync(Expression<Func<ProductGroup, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.ProductGroups
            .Where(predicate)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(cancellationToken);
    }
}