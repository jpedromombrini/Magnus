namespace Magnus.Application.Dtos.Requests;

public record CreateProductStockRequest(
    Guid ProductId,
    int Amount,
    int WarehouseId);