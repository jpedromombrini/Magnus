using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface IProductStockAppService
{
    Task<IEnumerable<ProductStockResponse>> GetProductStocksByFilterAsync(Guid productId, int warehouseId, bool all,
        CancellationToken cancellationToken);

    Task<int> GetBalanceProductStocksAsync(Guid productId, int warehouseId, CancellationToken cancellationToken);
    Task CreateProductStockMovementAsync(ProductStock productStock, CancellationToken cancellationToken);
}