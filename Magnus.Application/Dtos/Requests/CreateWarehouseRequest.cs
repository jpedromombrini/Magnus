namespace Magnus.Application.Dtos.Requests;

public record CreateWarehouseRequest(
    string Name,
    Guid UserId);