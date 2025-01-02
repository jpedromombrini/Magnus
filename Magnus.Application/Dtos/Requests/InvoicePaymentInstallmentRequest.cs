namespace Magnus.Application.Dtos.Requests;

public record InvoicePaymentInstallmentRequest(
    Guid InvoicePaymentId,
    DateOnly DueDate,
    DateTime? PaymentDate,
    decimal Value,
    decimal Discount,
    decimal Interest,
    int Installment
);