using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface IStockMovementAppService
{
    Task AddStockMovementAsync(CreateStockMovementRequest request, CancellationToken cancellationToken);
    Task UpdateStockMovementAsync(Guid id, UpdateStockMovementRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<StockMovementResponse>> GetStockMovementsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<StockMovementResponse>> GetStockMovementsByFilterAsync(GetStockMovementFilter filter,
        CancellationToken cancellationToken);

    Task<StockMovementResponse> GetStockMovementByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteStockMovementAsync(Guid id, CancellationToken cancellationToken);
}