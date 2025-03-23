namespace Magnus.Application.Dtos.Responses;

public record CostCenterGroupResponse(
    Guid Id,
    string Code,
    string Name,
    List<CostCenterSubGroupResponse>? CostCenterSubGroups);