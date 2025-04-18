using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IProductService
{
    Task CreateProductAsync(Product product, CancellationToken cancellationToken);
    Task<Product> UpdateProductAsync(Guid id, Product product, CancellationToken cancellationToken);
    Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
}