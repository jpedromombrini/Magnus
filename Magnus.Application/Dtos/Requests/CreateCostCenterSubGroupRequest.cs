namespace Magnus.Application.Dtos.Requests;

public record CreateCostCenterSubGroupRequest(
    Guid CostCenterGroupId,
    string Code,
    string Name);