using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IInvoiceService
{
    Task CreateInvoiceAsync(Invoice invoice, InvoicePayment invoicePayment, CancellationToken cancellationToken);
    Task DeleteInvoiceAsync(Invoice invoice, CancellationToken cancellationToken);
}