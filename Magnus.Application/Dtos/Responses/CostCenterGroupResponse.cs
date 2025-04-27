using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record CostCenterGroupResponse(
    Guid Id,
    string Code,
    string Name,
    CostcenterGroupType CostcenterGroupType,
    IEnumerable<CostCenterSubGroupResponse>? CostCenterSubGroups);