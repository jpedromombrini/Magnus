namespace Magnus.Application.Dtos.Responses;

public record InvoicePaymentResponse(
 Guid InvoiceId,
 Guid PaymentId);
