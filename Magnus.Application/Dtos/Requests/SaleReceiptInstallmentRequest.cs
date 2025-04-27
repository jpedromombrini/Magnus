namespace Magnus.Application.Dtos.Requests;

public record SaleReceiptInstallmentRequest(
    Guid SaleReceiptId,
    DateOnly DueDate,
    DateTime? PaymentDate,
    decimal PaymentValue,
    decimal Value,
    decimal Discount,
    decimal Interest,
    int Installment,
    string? ProofImage);