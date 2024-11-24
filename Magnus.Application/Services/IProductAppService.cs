using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services;

public interface IProductAppService
{
    Task AddProductAsync(CreateProductRequest request, CancellationToken cancellationToken);
    Task UpdateProductAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken);
    Task<ProductResponse> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteProductAsync(Guid id, CancellationToken cancellationToken);
    
}