using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record UpdateStatusTransferWarehouseItemRequest(
    Guid Id,
    int AutorizedAmount,
    TransferWarehouseItemStatus Status);