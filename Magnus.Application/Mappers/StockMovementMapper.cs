using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class StockMovementMapper
{
    #region Request

    public static StockMovement MapToEntity(this CreateStockMovementRequest request)
    {
        return new StockMovement(request.ProductId, request.Amount, request.AuditProductTypeEnum, request.WarehouseId,
            request.WarehouseName, request.UserId, request.Observation);
    }

    public static StockMovement MapToEntity(this UpdateStockMovementRequest request)
    {
        return new StockMovement(request.ProductId, request.Amount, request.AuditProductTypeEnum, request.WarehouseId,
            request.WarehouseName, request.UserId, request.Observation);
    }

    #endregion

    #region Response

    public static StockMovementResponse MapToResponse(this StockMovement entity)
    {
        return new StockMovementResponse(entity.Id, entity.CreatAt, entity.ProductId, entity.Product.Name,
            entity.Amount,
            entity.AuditProductType,
            entity.WarehouseId, entity.WarehouseName, entity.UserId, entity.Observation);
    }

    public static IEnumerable<StockMovementResponse> MapToResponse(this IEnumerable<StockMovement> entity)
    {
        return entity.Select(MapToResponse).ToList();
    }

    #endregion
}