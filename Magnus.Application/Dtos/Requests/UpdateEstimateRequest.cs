using System.Security.AccessControl;

namespace Magnus.Application.Dtos.Requests;

public record UpdateEstimateRequest(
    string Description,
    DateTime ValiditAt,
    Guid? ClientId,
    string? ClientName,
    decimal Value,
    decimal Freight,
    string Observation,
    Guid UserId,
    List<EstimateItemRequest> Items);