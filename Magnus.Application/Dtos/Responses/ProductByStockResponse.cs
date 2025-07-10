namespace Magnus.Application.Dtos.Responses;

public record ProductByStockResponse(
    Guid ProductId,
    string ProductName,
    IEnumerable<BarResponse> Bars,
    Guid WarehouseId,
    int Amount);