namespace Magnus.Application.Dtos.Requests;

public record UpdateWarehouseRequest(
    string Name,
    Guid UserId);