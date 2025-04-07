using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record CreateSaleRequest(
    DateTime CreateAt,
    int Document,
    Guid ClientId,
    Guid UserId,
    decimal Value,
    decimal FinantialDiscount,
    SaleStatus Status,
    List<SaleItemRequest> Items,
    List<CreateSaleReceiptRequest>? Receipts);