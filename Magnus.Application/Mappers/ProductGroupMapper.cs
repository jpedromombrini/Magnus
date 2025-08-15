using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class ProductGroupMapper
{
    #region Request

    public static ProductGroup MapToEntity(this CreateProductGroupRequest request)
    {
        return new ProductGroup(request.Name);
    }

    public static ProductGroup MapToEntity(this UpdateProductGroupRequest request)
    {
        return new ProductGroup(request.Name);
    }

    #endregion

    #region Response

    public static ProductGroupResponse MapToResponse(this ProductGroup entity)
    {
        return new ProductGroupResponse(entity.Id, entity.Name);
    }

    public static IEnumerable<ProductGroupResponse> MapToResponse(this IEnumerable<ProductGroup> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}