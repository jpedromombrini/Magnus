using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class SaleReceiptRepository(MagnusContext context) : Repository<SaleReceipt>(context), ISaleReceiptRepository
{
    private readonly MagnusContext _context = context;
    public override async Task<SaleReceipt?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.SaleReceipts
            .Include(x => x.Installments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<SaleReceipt>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.SaleReceipts
            .AsNoTracking()
            .Include(x => x.Installments)
            .ToListAsync(cancellationToken);
    }

    public override async Task<SaleReceipt?> GetByExpressionAsync(Expression<Func<SaleReceipt, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.SaleReceipts
            .Include(x => x.Installments)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public override async Task<IEnumerable<SaleReceipt>> GetAllByExpressionAsync(Expression<Func<SaleReceipt, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.SaleReceipts
            .AsNoTracking()
            .Where(predicate)
            .Include(x => x.Installments)
            .ToListAsync(cancellationToken);
        
    }
}