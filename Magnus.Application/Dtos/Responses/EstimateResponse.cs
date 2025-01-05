namespace Magnus.Application.Dtos.Responses;

public record EstimateResponse(
    Guid Id,
    int Code,
    string Description,
    DateTime CreatedAt,
    DateTime ValiditAt,
    Guid? ClientId,
    string? ClientName,
    decimal Value,
    decimal Freight,
    string Observation,
    Guid UserId,
    UserResponse User,
    List<EstimateItemResponse> Items
);