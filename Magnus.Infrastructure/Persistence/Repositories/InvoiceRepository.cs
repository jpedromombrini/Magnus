using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class InvoiceRepository(MagnusContext context) : Repository<Invoice>(context), IInvoiceRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<Invoice?> GetByExpressionAsync(Expression<Func<Invoice, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Invoices.AsNoTracking()
            .Where(predicate)
            .Include(x => x.Items)
            .Include(x => x.InvoicePayments)
            .ThenInclude(x => x.Installments)
            .Include(x => x.InvoicePayments)
            .ThenInclude(x => x.Payment)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Invoice>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Invoices.AsNoTracking()
            .Include(x => x.Items)
            .Include(x => x.InvoicePayments)
            .ThenInclude(x => x.Installments)
            .Include(x => x.InvoicePayments)
            .ThenInclude(x => x.Payment)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Invoices
            .Include(x => x.Items)
            .Include(x => x.InvoicePayments)
            .ThenInclude(x => x.Installments)
            .Include(x => x.InvoicePayments)
            .ThenInclude(x => x.Payment)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<Invoice>> GetAllByExpressionAsync(Expression<Func<Invoice, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Invoices.AsNoTracking()
            .Where(predicate)
            .Include(x => x.Items)
            .Include(x => x.InvoicePayments)
            .ThenInclude(x => x.Installments)
            .Include(x => x.InvoicePayments)
            .ThenInclude(x => x.Payment)
            .ToListAsync(cancellationToken);
    }
}