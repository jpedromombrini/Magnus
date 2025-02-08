namespace Magnus.Application.Dtos.Responses;

public record SaleReceiptResponse(
    Guid Id,
    Guid SaleId,
    Guid ReceiptId,
    List<SaleReceiptInstallmentResponse> Installments);