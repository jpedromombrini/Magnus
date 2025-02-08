namespace Magnus.Application.Dtos.Requests;

public record SaleReceiptRequest(
    Guid SaleId,
    Guid ReceiptId,
    List<SaleReceiptInstallmentRequest> Installments
);