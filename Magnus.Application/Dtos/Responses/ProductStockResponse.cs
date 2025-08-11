namespace Magnus.Application.Dtos.Responses;

public record ProductStockResponse(
    Guid Id,
    ProductResponse Product,
    decimal Amount,
    int WarehouseId,
    string WarehouseName);