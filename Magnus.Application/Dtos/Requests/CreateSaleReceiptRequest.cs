namespace Magnus.Application.Dtos.Requests;

public record CreateSaleReceiptRequest(
    Guid ClientId, 
    Guid UserId, 
    int Document,
    Guid SaleId,
    Guid ReceiptId,
    IEnumerable<SaleReceiptInstallmentRequest> Installments);