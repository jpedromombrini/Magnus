namespace Magnus.Application.Dtos.Responses;

public record InvoicePaymentResponse(
    Guid Id,
    Guid InvoiceId,
    Guid PaymentId,
    PaymentResponse Payment,
    Guid SupplierId,
    IEnumerable<InvoicePaymentInstallmentResponse> Installments);