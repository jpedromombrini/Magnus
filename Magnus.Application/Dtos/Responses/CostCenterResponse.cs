namespace Magnus.Application.Dtos.Responses;

public record CostCenterResponse(
    string Code,
    string Name,
    Guid? CostCenterSubGroupId,
    CostCenterSubGroupResponse? CostCenterSubGroup);