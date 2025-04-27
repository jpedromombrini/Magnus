using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record UpdateCostCenterGroupRequest(
    string Code,
    string Name,
    CostcenterGroupType CostcenterGroupType);