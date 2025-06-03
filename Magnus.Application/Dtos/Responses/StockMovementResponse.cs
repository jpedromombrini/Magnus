using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record StockMovementResponse(
    Guid Id,
    Guid ProductId,
    int Amount,
    AuditProductType AuditProductType,
    int WarehouseId,
    string WarehouseName,
    Guid UserId,
    string Observation);