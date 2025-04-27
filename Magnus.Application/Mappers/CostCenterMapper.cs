using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class CostCenterMapper
{
    #region Request

    public static CostCenter MapToEntity(this CreateCostCenterRequest request)
    {
        return new CostCenter(request.Code, request.Name, request.CostCenterSubGroupId);
    }

    public static CostCenter MapToEntity(this UpdateCostCenterRequest request)
    {
        return new CostCenter(request.Code, request.Name, request.CostCenterSubGroupId);
    }

    public static IEnumerable<CostCenter> MapToEntity(this IEnumerable<CreateCostCenterRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    public static IEnumerable<CostCenter> MapToEntity(this IEnumerable<UpdateCostCenterRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    #endregion

    #region Response

    public static CostCenterResponse MapToResponse(this CostCenter entity)
    {
        var costCenterGroup = new CostCenterGroupResponse(entity.CostCenterSubGroup.CostCenterGroupId,
            entity.CostCenterSubGroup.CostCenterGroup.Code, entity.CostCenterSubGroup.CostCenterGroup.Name,
            entity.CostCenterSubGroup.CostCenterGroup.CostcenterGroupType, null);
        var costCenterSubGroup = new CostCenterSubGroupResponse(entity.CostCenterSubGroupId,
            entity.CostCenterSubGroup.Code, entity.CostCenterSubGroup.Name, costCenterGroup.Id, costCenterGroup, null);
        return new CostCenterResponse(entity.Id, entity.Code, entity.Name, entity.CostCenterSubGroupId,
            costCenterSubGroup);
    }

    public static IEnumerable<CostCenterResponse> MapToResponse(this IEnumerable<CostCenter> requests)
    {
        return requests.Select(MapToResponse).ToList();
    }

    #endregion
}