using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface IProductStockAppService
{
    Task<IEnumerable<ProductStockResponse>> GetProductStocksByFilterAsync(Guid productId, int warehouseId, bool all,
        CancellationToken cancellationToken);

    Task<int> GetBalanceProductStocksAsync(Guid productId, int warehouseId, CancellationToken cancellationToken);
    Task CreateProductStockMovementAsync(CreateProductStockRequest request, CancellationToken cancellationToken);
}