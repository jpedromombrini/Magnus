using System.Security.AccessControl;

namespace Magnus.Application.Dtos.Requests;

public record UpdateEstimateRequest(
    Guid Id,
    string Description,
    DateTime ValiditAt,
    Guid? ClientId,
    string? ClientName,
    decimal Value,
    decimal Freight,
    decimal FinantialDiscount,
    string Observation,
    Guid UserId,
    IEnumerable<EstimateItemRequest> Items,
    IEnumerable<EstimateReceiptRequest>? Receipts);