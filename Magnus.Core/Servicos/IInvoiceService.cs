using Magnus.Core.Entities;

namespace Magnus.Core.Servicos;

public interface IInvoiceService
{
    Task CreateInvoiceAsync(Invoice invoice, InvoicePayment invoicePayment, CancellationToken cancellationToken);
    Task DeleteInvoiceAsync(Invoice invoice, CancellationToken cancellationToken);
}