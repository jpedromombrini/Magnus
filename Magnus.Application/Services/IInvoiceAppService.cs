using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services;

public interface IInvoiceAppService 
{
    Task AddInvoiceAsync(CreateInvoiceRequest request, CancellationToken cancellationToken);
    Task UpdateInvoiceAsync(Guid id, UpdateInvoiceRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<InvoiceResponse>> GetInvoicesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<InvoiceResponse>> GetInvoicesByFilterAsync(Expression<Func<Invoice, bool>> predicate, CancellationToken cancellationToken);
    Task<InvoiceResponse> GetInvoiceByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteInvoiceAsync(Guid id, CancellationToken cancellationToken);
}