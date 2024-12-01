namespace Magnus.Application.Dtos.Requests;

public record AccountsPayableOccurrenceRequest(
    Guid UserId,
    string UserName,
    string Occurrence);