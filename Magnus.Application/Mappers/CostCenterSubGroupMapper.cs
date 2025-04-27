using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;

namespace Magnus.Application.Mappers;

public static class CostCenterSubGroupMapper
{
    #region Request

    public static CostCenterSubGroup MapToEntity(this CreateCostCenterSubGroupRequest request)
    {
        return new CostCenterSubGroup(request.Code, request.Name, request.CostCenterGroupId);
    }

    public static CostCenterSubGroup MapToEntity(this UpdateCostCenterSubGroupRequest request)
    {
        return new CostCenterSubGroup(request.Code, request.Name, request.CostCenterGroupId);
    }

    public static IEnumerable<CostCenterSubGroup> MapToEntity(
        this IEnumerable<CreateCostCenterSubGroupRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    public static IEnumerable<CostCenterSubGroup> MapToEntity(
        this IEnumerable<UpdateCostCenterSubGroupRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion

    #region Response

    public static CostCenterSubGroupResponse MapToResponse(this CostCenterSubGroup entity)
    {
        var costCenters = new List<CostCenterResponse>();
        var costCenterGroup = new CostCenterGroupResponse(entity.CostCenterGroupId, entity.CostCenterGroup.Code,
            entity.CostCenterGroup.Name, entity.CostCenterGroup.CostcenterGroupType, null);
        var costCenterSubGroup = new CostCenterSubGroupResponse(entity.Id, entity.Code, entity.Name, entity.CostCenterGroupId,
            costCenterGroup,
            null);
        costCenters.AddRange(entity.CostCenters.Select(costCenter =>
            new CostCenterResponse(costCenter.Id, costCenter.Code, costCenter.Name, entity.Id, costCenterSubGroup)));
        return new CostCenterSubGroupResponse(entity.Id, entity.Code, entity.Name, entity.CostCenterGroupId,
            costCenterGroup,
            costCenters);
    }

    public static IEnumerable<CostCenterSubGroupResponse> MapToResponse(this IEnumerable<CostCenterSubGroup> requests)
    {
        return requests.Select(MapToResponse);
    }

    #endregion
}