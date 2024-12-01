namespace Magnus.Application.Dtos.Requests;

public record CreateCostCenterRequest(
    Guid CostCenterSubGroupId,
    string Code,
    string Name);