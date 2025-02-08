namespace Magnus.Application.Dtos.Requests;

public record UpdateSaleRequest(
    DateTime CreateAt,
    int Document,
    Guid ClientId,
    string ClientName,
    Guid UserId,
    decimal Value,
    decimal FinantialDiscount,
    List<SaleItemRequest> Items,
    List<SaleReceiptRequest> Receipts);