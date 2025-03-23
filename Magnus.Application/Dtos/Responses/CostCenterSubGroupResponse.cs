namespace Magnus.Application.Dtos.Responses;

public record CostCenterSubGroupResponse(
    Guid Id,
    string Code,
    string Name,
    Guid? CostCenterGroupId,
    CostCenterGroupResponse? CostCenterGroup,
    List<CostCenterResponse>? CostCenters);