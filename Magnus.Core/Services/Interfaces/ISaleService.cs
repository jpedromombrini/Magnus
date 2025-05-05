using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ISaleService
{
    Task CreateAsync(Sale sale, CancellationToken cancellationToken);

    Task UpdateSale(Guid id, Sale sale, CancellationToken cancellationToken);

    Task Invoice(Sale sale, CancellationToken cancellationToken);

    Task<Sale?> GetSaleByDocument(int documentId, CancellationToken cancellationToken);
    
    Task CancelSale(Sale sale, CancellationToken cancellationToken);
}