namespace Magnus.Application.Dtos.Responses;

public record DoctorResponse(
    Guid Id,
    int Code,
    string Name,
    string Crm);