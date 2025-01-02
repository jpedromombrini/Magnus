using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services;

public interface IInvoicePaymentAppService
{
    Task<IEnumerable<InvoicePaymentResponse>> GetInvoicePaymentsAsync(CancellationToken cancellationToken);
    Task<InvoicePaymentResponse> GetInvoicePaymentsByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken);
}