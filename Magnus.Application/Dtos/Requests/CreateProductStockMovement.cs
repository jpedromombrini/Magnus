namespace Magnus.Application.Dtos.Requests;

public record CreateProductStockMovement(
    Guid ProductId,
    int Amount,
    int WarehouseDestinyCode);