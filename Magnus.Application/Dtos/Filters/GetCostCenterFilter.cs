using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Filters;

public record GetCostCenterFilter(
    string? Name,
    CostcenterGroupType? CostCenterGroupType);
