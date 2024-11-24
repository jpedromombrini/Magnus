namespace Magnus.Application.Dtos.Responses;

public record UserResponse(
    Guid Id,
    string Email,
    int UserType,
    string Name,
    DateTime InitialDate,
    DateTime FinalDate,
    bool Active);