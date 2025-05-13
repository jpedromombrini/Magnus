namespace Magnus.Application.Dtos.Requests;

public record UpdateSaleRequest(
    DateTime CreateAt,
    int Document,
    Guid ClientId,
    string ClientName,
    Guid UserId,
    decimal Value,
    Guid? FreightId,
    decimal Freight,
    decimal FinantialDiscount,
    IEnumerable<SaleItemRequest> Items,
    IEnumerable<UpdateSaleReceiptRequest>? Receipts);