using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IStockMovementService
{
    Task CreateStockMovementAsync(StockMovement stockMovement, CancellationToken cancellationToken);
}