using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface ISaleReceiptAppService
{
    Task<IEnumerable<SaleReceiptResponse>> GetSaleReceiptsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<SaleReceiptResponse>> GetSaleReceiptsByFilterAsync(GetSaleReceiptFilter filter, CancellationToken cancellationToken);
    Task<IEnumerable<SaleReceiptResponse>> ListSaleReceiptByIdAsync(Guid saleId, CancellationToken cancellationToken);
    Task DeleteSaleReceiptAsync(Guid id, CancellationToken cancellationToken);
}