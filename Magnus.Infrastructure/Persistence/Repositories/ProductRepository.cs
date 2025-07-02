using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class ProductRepository(MagnusContext context) : Repository<Product>(context), IProductRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<IEnumerable<Product>> GetAllByExpressionAsync(Expression<Func<Product, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(predicate)
            .Include(x => x.Bars)
            .Include(x => x.ProductPriceTables)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(x => x.Bars)
            .Include(x => x.ProductPriceTables)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(x => x.Bars)
            .Include(x => x.ProductPriceTables)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void DeleteBarsRange(Guid productId)
    {
        var bars = _context.Bars.Where(x => x.ProductId == productId);
        _context.Bars.RemoveRange(bars);
    }

    public void DeleteProductPriceTableRange(Guid productId)
    {
        var tables = _context.ProductPriceTables.Where(x => x.ProductId == productId);
        _context.ProductPriceTables.RemoveRange(tables);
    }
}