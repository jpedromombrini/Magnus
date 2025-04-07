using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services;

public interface ISaleAppService
{
    Task AddSaleAsync(CreateSaleRequest request, CancellationToken cancellationToken);
    Task UpdateSaleAsync(Guid id, UpdateSaleRequest request, CancellationToken cancellationToken);
    Task InvoiceSaleAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<SaleResponse>> GetSalesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<SaleResponse>> GetSalesByFilterAsync(GetSaleFilter filter, CancellationToken cancellationToken);
    Task<SaleResponse> GetSaleByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteSaleAsync(Guid id, CancellationToken cancellationToken);
}