namespace Magnus.Application.Dtos.Responses;

public record TransferWarehouseResponse(
    Guid Id,
    Guid UserId,
    string UserName,
    int WarehouseOriginId,
    string WarehouseOriginName,
    int WarehouseDestinyId,
    string WarehouseDestinyName,
    List<TransferWarehouseItemResponse> Items);