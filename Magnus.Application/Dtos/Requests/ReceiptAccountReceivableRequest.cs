namespace Magnus.Application.Dtos.Requests;

public record ReceiptAccountReceivableRequest(
    decimal Value,
    decimal Interest,
    decimal Discount,
    decimal PaymentValue,
    DateTime PaymentDate,
    string? ProofImage);