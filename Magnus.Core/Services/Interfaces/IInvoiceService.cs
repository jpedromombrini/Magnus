using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IInvoiceService
{
    Task CreateInvoiceAsync(Invoice invoice, CancellationToken cancellationToken);
    Task DeleteInvoiceAsync(Invoice invoice, CancellationToken cancellationToken);
}