using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface ISellerAppService
{
    Task AddSellerAsync(CreateSellerRequest request, CancellationToken cancellationToken);
    Task UpdateSellerAsync(Guid id, UpdateSellerRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<SellerResponse>> GetSellersAsync(CancellationToken cancellationToken);
    Task<IEnumerable<SellerResponse>> GetSellersByFilterAsync(Expression<Func<Seller, bool>> predicate, CancellationToken cancellationToken);
    Task<SellerResponse> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteSellerAsync(Guid id, CancellationToken cancellationToken);
}