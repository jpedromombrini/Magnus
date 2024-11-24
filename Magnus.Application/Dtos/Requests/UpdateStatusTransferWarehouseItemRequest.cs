using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record UpdateStatusTransferWarehouseItemRequest(
    Guid Id,
    TransferWarehouseItemStatus Status);