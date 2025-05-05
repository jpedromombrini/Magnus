namespace Magnus.Application.Dtos.Responses;

public record TransferWarehouseResponse(
    Guid Id,
    Guid UserId,
    DateTime CreatedAt,
    string UserName,
    int WarehouseOriginId,
    string WarehouseOriginName,
    int WarehouseDestinyId,
    string WarehouseDestinyName,
    IEnumerable<TransferWarehouseItemResponse> Items);