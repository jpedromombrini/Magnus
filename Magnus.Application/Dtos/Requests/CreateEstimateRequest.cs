namespace Magnus.Application.Dtos.Requests;

public record CreateEstimateRequest(
    string? Description,
    DateTime ValiditAt,
    Guid? ClientId,
    string? ClientName,
    decimal Value,
    Guid? FreightId,
    decimal Freight,
    decimal FinantialDiscount,
    string? Observation,
    Guid UserId,
    IEnumerable<EstimateItemRequest> Items,
    IEnumerable<EstimateReceiptRequest>? Receipts);