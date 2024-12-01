using Magnus.Core.Entities;

namespace Magnus.Core.Servicos;

public interface IInvoiceService
{
    Task CreateInvoiceAsync(Invoice invoice, List<AccountsPayable> accountsPayables, CancellationToken cancellationToken);
    Task DeleteInvoiceAsync(Invoice invoice, CancellationToken cancellationToken);
}