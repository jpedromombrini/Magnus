using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ISaleService
{
    Task<Sale> CreateSaleByEstimateAsync(Estimate estimate, CancellationToken cancellationToken);
    Task CreateAsync(Sale sale, CancellationToken cancellationToken);

    Task UpdateSale(Guid id, Sale sale, CancellationToken cancellationToken);

    Task Invoice(Sale sale, Client client, CancellationToken cancellationToken);

    Task<Sale?> GetSaleByDocument(int documentId, CancellationToken cancellationToken);

    Task CancelSale(Sale sale, string reason, CancellationToken cancellationToken);
}