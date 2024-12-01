using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class InvoiceRepository(MagnusContext context) : Repository<Invoice>(context), IInvoiceRepository
{
    private readonly MagnusContext _context = context;
    public async Task<Invoice?> GetByExpressionAsync(Expression<Func<Invoice, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Invoices.FirstOrDefaultAsync(predicate, cancellationToken);
    }
}