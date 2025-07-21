namespace Magnus.Application.Dtos.Responses;

public record WarehouseStockResponse(
    int Code,
    Guid WarehouseId,
    string WarehouseName,
    List<ProductByStockResponse> Products);