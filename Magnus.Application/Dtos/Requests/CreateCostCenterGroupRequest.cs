namespace Magnus.Application.Dtos.Requests;

public record CreateCostCenterGroupRequest(
    string Code,
    string Name);