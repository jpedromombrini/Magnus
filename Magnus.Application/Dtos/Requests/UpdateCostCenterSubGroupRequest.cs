namespace Magnus.Application.Dtos.Requests;

public record UpdateCostCenterSubGroupRequest(
    Guid CostCenterGroupId,
    string Code,
    string Name);