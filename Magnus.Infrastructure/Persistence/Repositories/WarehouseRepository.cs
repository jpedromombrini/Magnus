using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class WarehouseRepository(MagnusContext context) : Repository<Warehouse>(context), IWarehouseRepository
{
    private readonly MagnusContext _context = context;
    public override async Task<IEnumerable<Warehouse>> GetAllByExpressionAsync(Expression<Func<Warehouse, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Warehouses.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }
}