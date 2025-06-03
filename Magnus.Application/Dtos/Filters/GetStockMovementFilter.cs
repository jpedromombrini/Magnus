using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Filters;

public record GetStockMovementFilter(
    DateTime InitialDate,
    DateTime FinalDate,
    Guid? ProductId,
    Guid? UserId,
    AuditProductType? AuditProductType);