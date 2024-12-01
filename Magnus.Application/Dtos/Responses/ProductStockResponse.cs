namespace Magnus.Application.Dtos.Responses;

public record ProductStockResponse(
    Guid ProductId,
    DateOnly ValidityDate,
    decimal Amount,
    int WarehouseId,
    string WarehouseName);