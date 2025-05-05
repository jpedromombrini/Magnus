namespace Magnus.Application.Dtos.Requests;

public record InvoicePaymentRequest(
    Guid PaymentId,
    IEnumerable<InvoicePaymentInstallmentRequest> Installments);