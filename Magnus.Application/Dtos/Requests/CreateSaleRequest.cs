using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record CreateSaleRequest(
    DateTime CreateAt,
    int Document,
    Guid ClientId,
    Guid UserId,
    decimal Value,
    decimal Freight,
    decimal FinantialDiscount,
    SaleStatus Status,
    IEnumerable<SaleItemRequest> Items,
    IEnumerable<CreateSaleReceiptRequest>? Receipts);