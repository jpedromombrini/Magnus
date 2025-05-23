namespace Magnus.Application.Dtos.Responses;

public record InvoicePaymentInstallmentResponse(
    Guid Id,
    Guid InvoicePaymentId,
    DateOnly DueDate,
    DateTime? PaymentDate,
    decimal Value,
    decimal Discount,
    decimal Interest,
    int Installment);