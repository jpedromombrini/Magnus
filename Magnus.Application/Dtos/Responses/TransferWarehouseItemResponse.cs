using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record TransferWarehouseItemResponse(
    Guid Id,
    Guid ProductId,
    int ProductInternalCode,
    string ProductName,
    int Amount,
    Guid TransferWarehouseId,
    TransferWarehouseItemStatus Status,
    string TransferWarehouseOriginName,
    string TransferWarehouseDestinyName);