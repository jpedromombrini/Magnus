using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface IInvoicePaymentAppService
{
    Task<IEnumerable<InvoicePaymentResponse>> GetInvoicePaymentsAsync(CancellationToken cancellationToken);
    Task<InvoicePaymentResponse> GetInvoicePaymentsByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken);
}