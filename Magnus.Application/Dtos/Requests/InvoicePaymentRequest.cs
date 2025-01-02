namespace Magnus.Application.Dtos.Requests;

public record InvoicePaymentRequest(
    Guid PaymentId,
    List<InvoicePaymentInstallmentRequest> Installments);