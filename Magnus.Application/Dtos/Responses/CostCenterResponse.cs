namespace Magnus.Application.Dtos.Responses;

public record CostCenterResponse(
    Guid Id,
    string Code,
    string Name,
    Guid CostCenterSubGroupId,
    CostCenterSubGroupResponse CostCenterSubGroup);