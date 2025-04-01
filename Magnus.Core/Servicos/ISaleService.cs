using Magnus.Core.Entities;

namespace Magnus.Core.Servicos;

public interface ISaleService
{
    void CreateSale(Sale sale, Client client, User user);

    void UpdateSale(Sale sale, Client client, User user, IEnumerable<SaleItem> items, IEnumerable<SaleReceipt> receipts,
        decimal value, decimal finantialDiscount);

    Task Invoice(Sale sale, CancellationToken cancellationToken);
}