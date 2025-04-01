using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class TransferWarehouseRepository(MagnusContext context)
    : Repository<TransferWarehouse>(context), ITransferWarehouseRepository
{
    private readonly MagnusContext _context = context;

    public void UpdateStatusAsync(TransferWarehouseItem item)
    {
        _context.TransferWarehouseItems.Update(item);
    }

    public async Task<TransferWarehouseItem?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TransferWarehouseItems
            .Include(x => x.TransferWarehouse)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TransferWarehouseItem>> GetItemsByStatusAsync(
        Expression<Func<TransferWarehouseItem, bool>> predicate, CancellationToken cancellationToken)
    {
        var data = await _context.TransferWarehouseItems.Where(predicate)
            .Include(x => x.TransferWarehouse)
            .ToListAsync(cancellationToken);

        return data;
    }

    public override async Task<IEnumerable<TransferWarehouse>> GetAllByExpressionAsync(
        Expression<Func<TransferWarehouse, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.TransferWarehouses
            .AsNoTracking()
            .Where(predicate)
            .Include(x => x.Items)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<TransferWarehouse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.TransferWarehouses
            .AsNoTracking()
            .Include(x => x.Items)
            .ToListAsync(cancellationToken);
    }

    public override async Task<TransferWarehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TransferWarehouses
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}