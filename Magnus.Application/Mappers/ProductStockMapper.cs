using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class ProductStockMapper
{
    #region Request

    public static ProductStock MapToEntity(this CreateProductStockRequest request)
    {
        return new ProductStock(request.ProductId, request.Amount, request.WarehouseId);
    }

    #endregion

    #region Response

    public static ProductStockResponse MapToResponse(this ProductStock entity)
    {
        return new ProductStockResponse(entity.Id, entity.Product.MapToResponse(), entity.Amount, entity.WarehouseId,
            entity.WarehouseName);
    }

    public static IEnumerable<ProductStockResponse> MapToResponse(this IEnumerable<ProductStock> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}