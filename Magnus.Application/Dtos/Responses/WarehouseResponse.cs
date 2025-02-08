namespace Magnus.Application.Dtos.Responses;

public record WarehouseResponse(
    Guid Id,
    int Code,
    string Name,
    Guid UserId);

