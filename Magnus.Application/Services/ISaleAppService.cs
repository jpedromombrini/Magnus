using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services;

public interface ISaleAppService
{
    Task AddSaleAsync(CreateSaleRequest request, CancellationToken cancellationToken);
    Task UpdateSaleAsync(Guid id, UpdateSaleRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<SaleResponse>> GetSalesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<SaleResponse>> GetSalesByFilterAsync(Expression<Func<Sale, bool>> predicate, CancellationToken cancellationToken);
    Task<SaleResponse> GetSaleByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteSaleAsync(Guid id, CancellationToken cancellationToken);
}