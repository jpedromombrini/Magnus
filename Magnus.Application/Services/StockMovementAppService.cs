using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class StockMovementAppService(
    IStockMovementService stockMovementService,
    IUnitOfWork unitOfWork) : IStockMovementAppService
{
    public async Task AddStockMovementAsync(CreateStockMovementRequest request, CancellationToken cancellationToken)
    {
        await stockMovementService.CreateStockMovementAsync(request.MapToEntity(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateStockMovementAsync(Guid id, UpdateStockMovementRequest request,
        CancellationToken cancellationToken)
    {
        var stockDb = await unitOfWork.StockMovements.GetByIdAsync(id, cancellationToken);
        if (stockDb == null)
            throw new EntityNotFoundException("Nenhuma movimentação encontrada com esse id");
        stockDb.SetObservation(request.Observation);
        stockDb.SetProductId(request.ProductId);
        stockDb.SetAmount(request.Amount);
        stockDb.SetAuditProductType(request.AuditProductType);
        stockDb.SetWarehouseId(request.WarehouseId);
        stockDb.SetWarehouseName(request.WarehouseName);
        unitOfWork.StockMovements.Update(stockDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<StockMovementResponse>> GetStockMovementsAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.StockMovements.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<StockMovementResponse>> GetStockMovementsByFilterAsync(GetStockMovementFilter filter,
        CancellationToken cancellationToken)
    {
        var stocks = await unitOfWork.StockMovements.GetAllByExpressionAsync(
            x => (filter == null || x.ProductId == filter.ProductId) &&
                 (filter.AuditProductType == null || x.AuditProductType == filter.AuditProductType) &&
                 (filter.UserId == null || x.UserId == filter.UserId) &&
                 x.CreatAt >= filter.InitialDate && x.CreatAt <= filter.FinalDate, cancellationToken);
        return stocks.MapToResponse();
    }

    public async Task<StockMovementResponse> GetStockMovementByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var stockDb = await unitOfWork.StockMovements.GetByIdAsync(id, cancellationToken);
        if (stockDb == null)
            throw new EntityNotFoundException("Nenhuma movimentação encontrada com esse id");
        return stockDb.MapToResponse();
    }

    public async Task DeleteStockMovementAsync(Guid id, CancellationToken cancellationToken)
    {
        var stockDb = await unitOfWork.StockMovements.GetByIdAsync(id, cancellationToken);
        if (stockDb == null)
            throw new EntityNotFoundException("Nenhuma movimentação encontrada com esse id");
        unitOfWork.StockMovements.Delete(stockDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}