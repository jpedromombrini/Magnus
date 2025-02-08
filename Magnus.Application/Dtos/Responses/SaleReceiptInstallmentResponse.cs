namespace Magnus.Application.Dtos.Responses;

public record SaleReceiptInstallmentResponse(
    Guid Id,
    Guid SaleReceiptId,
    DateOnly DueDate,
    DateTime? PaymentDate,
    decimal Value,
    decimal Discount,
    decimal Interest,
    int Installment);