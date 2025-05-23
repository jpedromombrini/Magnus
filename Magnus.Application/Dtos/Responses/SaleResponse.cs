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
    Guid? FreightId,
    decimal Freight,
    decimal FinantialDiscount,
    string ReasonCancel,
    SaleStatus Status,
    IEnumerable<SaleItemResponse> Items,
    IEnumerable<SaleReceiptResponse>? Receipts);