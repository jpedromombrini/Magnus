using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface IProductGroupAppService
{
    Task AddProductGroupAsync(CreateProductGroupRequest request, CancellationToken cancellationToken);
    Task UpdateProductGroupAsync(Guid id, UpdateProductGroupRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<ProductGroupResponse>> GetProductGroupsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<ProductGroupResponse>> GetProductGroupsByFilterAsync(
        Expression<Func<ProductGroup, bool>> predicate, CancellationToken cancellationToken);

    Task<ProductGroupResponse> GetProductGroupByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteProductGroupAsync(Guid id, CancellationToken cancellationToken);
}