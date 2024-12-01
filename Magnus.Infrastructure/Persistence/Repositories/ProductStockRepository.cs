using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class ProductStockRepository(MagnusContext context) : Repository<ProductStock>(context), IProductStockRepository
{
    private readonly MagnusContext _context = context;
    public async Task<ProductStock?> GetByExpressionAsync(Expression<Func<ProductStock, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.ProductStocks.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task AddRangeAsync(List<ProductStock> productStocks, CancellationToken cancellationToken)
    {
        await _context.ProductStocks.AddRangeAsync(productStocks, cancellationToken);
    }
}