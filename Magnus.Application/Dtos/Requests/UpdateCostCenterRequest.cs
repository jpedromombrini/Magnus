namespace Magnus.Application.Dtos.Requests;

public record UpdateCostCenterRequest(
    Guid CostCenterSubGroupId,
    string Code,
    string Name);