namespace Magnus.Application.Dtos.Responses;

public record WarehouseStockResponse(
    Guid WarehouseId,
    string WarehouseName,
    List<ProductByStockResponse> Products);