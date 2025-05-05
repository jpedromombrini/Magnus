using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ISellerService
{
    Task AddSellerAsync(Seller seller, string password, CancellationToken cancellationToken);
    Task UpdateSellerAsync(Guid id, Seller seller, CancellationToken cancellationToken);
}