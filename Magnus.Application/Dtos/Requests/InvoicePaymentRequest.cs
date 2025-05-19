namespace Magnus.Application.Dtos.Requests;

public record InvoicePaymentRequest(
    Guid PaymentId,
    Guid SupplierId,
    IEnumerable<InvoicePaymentInstallmentRequest> Installments);