namespace Magnus.Application.Dtos.Responses;

public record AccountsReceivableOccurenceResponse(
    DateTime CreatAt,
    Guid AccountsReceivableId,
    Guid UserId,
    UserResponse User,
    string Occurrence);