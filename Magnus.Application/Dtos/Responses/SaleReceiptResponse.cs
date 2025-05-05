namespace Magnus.Application.Dtos.Responses;

public record SaleReceiptResponse(
    Guid Id,
    Guid ClientId,
    Guid UserId,
    Guid SaleId,
    Guid ReceiptId,
    IEnumerable<SaleReceiptInstallmentResponse> Installments);