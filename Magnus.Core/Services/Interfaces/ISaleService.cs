using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ISaleService
{
    Task CreateAsync(Sale sale, CancellationToken cancellationToken);

    Task UpdateSale(Sale sale, Client client, User user, IEnumerable<SaleItem> items, IEnumerable<SaleReceipt> receipts,
        decimal value, decimal finantialDiscount, CancellationToken cancellationToken);

    Task Invoice(Sale sale, CancellationToken cancellationToken);

    Task<Sale?> GetSaleByDocument(int documentId, CancellationToken cancellationToken);
}