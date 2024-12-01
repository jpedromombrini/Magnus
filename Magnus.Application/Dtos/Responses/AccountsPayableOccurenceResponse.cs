namespace Magnus.Application.Dtos.Responses;

public record AccountsPayableOccurenceResponse(
    Guid AccountsPayableId,
    Guid UserId,
    string UserName,
    string Occurrence
);