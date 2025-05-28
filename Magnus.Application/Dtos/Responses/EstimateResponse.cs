using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record EstimateResponse(
    Guid Id,
    int Code,
    string Description,
    DateTime CreatedAt,
    DateTime ValiditAt,
    Guid? ClientId,
    string? ClientName,
    decimal Value,
    Guid? FreightId,
    decimal Freight,
    decimal FinantialDiscount,
    string Observation,
    Guid UserId,
    UserResponse User,
    IEnumerable<EstimateItemResponse> Items,
    IEnumerable<EstimateReceiptResponse>? Receipts,
    EstimateStatus EstimateStatus);