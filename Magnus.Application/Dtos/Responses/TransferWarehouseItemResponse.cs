namespace Magnus.Application.Dtos.Responses;

public record TransferWarehouseItemResponse(
    Guid Id,
    Guid ProductId,
    int ProductInternalCode,
    string ProductName,
    decimal Amount,
    string TransferWarehouseOriginName,
    string TransferWarehouseDestinyName);