namespace Magnus.Application.Dtos.Responses;

public record EstimateReceiptInstallmentResponse(
    Guid Id,
    Guid EstimateReceiptId,
    DateOnly DueDate,
    DateTime? PaymentDate,
    decimal PaymentValue,
    decimal Value,
    decimal Discount,
    decimal Interest,
    int Installment);