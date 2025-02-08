using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class ReceiptRepository(MagnusContext context) : Repository<Receipt>(context), IReceiptRepository
{
    private readonly MagnusContext _context = context;
    public override async Task<IEnumerable<Receipt>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Receipts
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}