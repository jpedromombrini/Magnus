using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class CostCenterGroupMapper
{
    #region Request

    public static CostCenterGroup MapToEntity(this CreateCostCenterGroupRequest request)
    {
        return new CostCenterGroup(request.Code, request.Name, request.CostcenterGroupType);
    }

    public static CostCenterGroup MapToEntity(this UpdateCostCenterGroupRequest request)
    {
        return new CostCenterGroup(request.Code, request.Name, request.CostcenterGroupType);
    }

    public static IEnumerable<CostCenterGroup> MapToEntity(this IEnumerable<CreateCostCenterGroupRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    public static IEnumerable<CostCenterGroup> MapToEntity(this IEnumerable<UpdateCostCenterGroupRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion

    #region Response

    public static CostCenterGroupResponse MapToResponse(this CostCenterGroup entity)
    {
        IEnumerable<CostCenterSubGroupResponse> subGroups = [];
        if (entity.CostCenterSubGroups is not null)
            subGroups = entity.CostCenterSubGroups.MapToResponse();

        return new CostCenterGroupResponse(entity.Id, entity.Code, entity.Name, entity.CostcenterGroupType, subGroups);
    }

    public static IEnumerable<CostCenterGroupResponse> MapToResponse(this IEnumerable<CostCenterGroup> requests)
    {
        return requests.Select(MapToResponse);
    }

    #endregion
}