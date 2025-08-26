using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class SaleRepository(MagnusContext context) : Repository<Sale>(context), ISaleRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Sales
            .Include(x => x.Items)
            .Include(x => x.Receipts)
            .ThenInclude(x => x.Receipt)
            .Include(x => x.Receipts)
            .ThenInclude(x => x.Installments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<Sale>> GetAllByExpressionAsync(Expression<Func<Sale, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Sales.AsNoTracking()
            .Where(predicate)
            .Include(x => x.Items)
            .Include(x => x.Receipts)
            .ThenInclude(x => x.Receipt)
            .Include(x => x.Receipts)
            .ThenInclude(x => x.Installments)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Sale?> GetByExpressionAsync(Expression<Func<Sale, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Sales
            .Where(predicate)
            .Include(x => x.Items)
            .Include(x => x.Receipts)
            .ThenInclude(x => x.Receipt)
            .Include(x => x.Receipts)
            .ThenInclude(x => x.Installments)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Sales
            .AsNoTracking()
            .Include(x => x.Items)
            .Include(x => x.Receipts)
            .ThenInclude(x => x.Receipt)
            .Include(x => x.Receipts)
            .ThenInclude(x => x.Installments)
            .ToListAsync(cancellationToken);
    }

    public void DeleteItensRange(IEnumerable<SaleItem> items)
    {
        _context.SaleItems.RemoveRange(items);
    }

    public void DeleteReceiptsRange(IEnumerable<SaleReceipt> receipts)
    {
        _context.SaleReceipts.RemoveRange(receipts);
    }

    public async Task AddItemsRangeAsync(IEnumerable<SaleItem> items, CancellationToken cancellationToken)
    {
        await _context.SaleItems.AddRangeAsync(items, cancellationToken);
    }

    public async Task AddReceiptsRangeAsync(IEnumerable<SaleReceipt> receipts, CancellationToken cancellationToken)
    {
        await _context.SaleReceipts.AddRangeAsync(receipts, cancellationToken);
    }
}