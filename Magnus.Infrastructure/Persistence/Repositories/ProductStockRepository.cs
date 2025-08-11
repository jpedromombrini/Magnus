using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class ProductStockRepository(MagnusContext context) : Repository<ProductStock>(context), IProductStockRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<ProductStock?> GetByExpressionAsync(Expression<Func<ProductStock, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.ProductStocks.Include(x => x.Product).FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task AddRangeAsync(List<ProductStock> productStocks, CancellationToken cancellationToken)
    {
        await _context.ProductStocks.AddRangeAsync(productStocks, cancellationToken);
    }

    public override async Task<IEnumerable<ProductStock>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ProductStocks.Include(x => x.Product).ToListAsync();
    }

    public override async Task<IEnumerable<ProductStock>> GetAllByExpressionAsync(
        Expression<Func<ProductStock, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.ProductStocks.Include(x => x.Product).Where(predicate).ToListAsync();
    }

    public override async Task<ProductStock?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ProductStocks.Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.ProductId == id, cancellationToken);
    }
}