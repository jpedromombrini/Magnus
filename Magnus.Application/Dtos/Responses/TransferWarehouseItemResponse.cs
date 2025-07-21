using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record TransferWarehouseItemResponse(
    Guid Id,
    DateTime Created,
    Guid ProductId,
    int ProductInternalCode,
    string ProductName,
    int RequestedAmount,
    int AutorizedAmount,
    Guid TransferWarehouseId,
    TransferWarehouseItemStatus Status,
    string TransferWarehouseOriginName,
    string TransferWarehouseDestinyName);