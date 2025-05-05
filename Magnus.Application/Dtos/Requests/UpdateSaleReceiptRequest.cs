namespace Magnus.Application.Dtos.Requests;

public record UpdateSaleReceiptRequest(
    Guid ClientId, 
    Guid UserId, 
    int Document,
    Guid SaleId,
    Guid ReceiptId,
    IEnumerable<SaleReceiptInstallmentRequest> Installments);