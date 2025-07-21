namespace Magnus.Application.Dtos.Requests;

public record PayAccountPayableRequest(
    decimal PaymentValue,
    DateTime PaymentDate,
    string? ProofImage);