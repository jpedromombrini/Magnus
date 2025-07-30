namespace Magnus.Application.Dtos.Responses;

public record SellerResponse(
    Guid Id,
    string Name,
    string? Document,
    string Phone,
    string Email,
    Guid? UserId);