using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IProductStockService
{
    Task<decimal> GetProductStockAsync(Guid productId, int warehouseId, CancellationToken cancellationToken);
    Task SubtractProductStockAsync(Guid productId, int warehouseId, int amount, CancellationToken cancellationToken);
    Task IncrementProductStockAsync(Guid productId, int warehouseId, int amount, CancellationToken cancellationToken);
    Task CreateProductStockMovementAsync(ProductStock productStock, CancellationToken cancellationToken);
}