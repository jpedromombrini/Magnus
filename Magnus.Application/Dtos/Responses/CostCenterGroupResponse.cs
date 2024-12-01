namespace Magnus.Application.Dtos.Responses;

public record CostCenterGroupResponse(
    string Code,
    string Name,
    List<CostCenterSubGroupResponse>? CostCenterSubGroups);