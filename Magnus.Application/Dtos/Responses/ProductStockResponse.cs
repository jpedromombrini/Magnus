namespace Magnus.Application.Dtos.Responses;

public record ProductStockResponse(
    Guid ProductId,
    decimal Amount,
    int WarehouseId,
    string WarehouseName);