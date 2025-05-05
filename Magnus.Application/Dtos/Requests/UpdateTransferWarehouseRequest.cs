namespace Magnus.Application.Dtos.Requests;

public record UpdateTransferWarehouseRequest(
    Guid UserId,
    string UserName,
    int WarehouseOriginId,
    string WarehouseOriginName,
    int WarehouseDestinyId,
    string WarehouseDestinyName,
    IEnumerable<TransferWarehouseItemRequest> Items);