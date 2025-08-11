namespace Magnus.Application.Dtos.Responses;

public record ProductByStockResponse(
    Guid ProductId,
    string ProductName,
    bool ApplyPriceRule,
    IEnumerable<BarResponse> Bars,
    Guid WarehouseId,
    int Amount);