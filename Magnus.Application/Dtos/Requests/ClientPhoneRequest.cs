namespace Magnus.Application.Dtos.Requests;

public record ClientPhoneRequest(
    string Number,
    string Description);