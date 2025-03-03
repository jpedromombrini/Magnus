using System.Linq.Expressions;
using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface IInvoiceAppService 
{
    Task AddInvoiceAsync(CreateInvoiceRequest request, CancellationToken cancellationToken);
    Task UpdateInvoiceAsync(Guid id, UpdateInvoiceRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<InvoiceResponse>> GetInvoicesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<InvoiceResponse>> GetInvoicesByFilterAsync(GetInvoiceFilter filter, CancellationToken cancellationToken);
    Task<InvoiceResponse> GetInvoiceByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteInvoiceAsync(Guid id, CancellationToken cancellationToken);
}