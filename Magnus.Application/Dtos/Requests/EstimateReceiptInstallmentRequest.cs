namespace Magnus.Application.Dtos.Requests;

public record EstimateReceiptInstallmentRequest(
    Guid EstimateReceiptId,
    DateOnly DueDate,
    DateTime? PaymentDate,
    decimal PaymentValue,
    decimal Value,
    decimal Discount,
    decimal Interest,
    int Installment);