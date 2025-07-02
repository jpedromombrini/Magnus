using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record StockMovementResponse(
    Guid Id,
    DateTime CreatAt,
    Guid ProductId,
    string ProductName,
    int Amount,
    AuditProductType AuditProductTypeEnum,
    int WarehouseId,
    string WarehouseName,
    Guid UserId,
    string Observation);