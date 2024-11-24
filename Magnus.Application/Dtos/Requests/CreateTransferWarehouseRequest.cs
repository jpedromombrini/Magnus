namespace Magnus.Application.Dtos.Requests;

public record CreateTransferWarehouseRequest(
    Guid UserId,
    string UserName,
    int WarehouseOriginId,
    string WarehouseOriginName,
    int WarehouseDestinyId,
    string WarehouseDestinyName,
    List<TransferWarehouseItemRequest> Items);