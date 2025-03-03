using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record SaleResponse(
    Guid Id,
    DateTime CreateAt,
    int Document,
    Guid ClientId,
    string ClientName,
    Guid UserId,
    decimal Value,
    decimal FinantialDiscount,
    SaleStatus Status,
    List<SaleItemResponse> Items,
    List<SaleReceiptResponse> Receipts);