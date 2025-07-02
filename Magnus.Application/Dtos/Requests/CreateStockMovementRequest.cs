using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record CreateStockMovementRequest(
    Guid ProductId,
    int Amount,
    AuditProductType AuditProductTypeEnum,
    int WarehouseId,
    string WarehouseName,
    Guid UserId,
    string Observation);