using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record CreateCostCenterGroupRequest(
    string Code,
    string Name,
    CostcenterGroupType CostcenterGroupType);