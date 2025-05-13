using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class FreightMapper
{
    #region Request

    public static Freight MapToEntity(this CreateFreightRequest request)
    {
        return new Freight(request.Name);
    }

    public static Freight MapToEntity(this UpdateFreightRequest request)
    {
        return new Freight(request.Name);
    }

    public static IEnumerable<Freight> MapToEntity(this IEnumerable<CreateFreightRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    public static IEnumerable<Freight> MapToEntity(this IEnumerable<UpdateFreightRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion

    #region Response

    public static FreightResponse MapResponse(this Freight entity)
    {
        return new FreightResponse(entity.Id, entity.Name);
    }

    public static IEnumerable<FreightResponse> MapToResponse(this IEnumerable<Freight> entities)
    {
        return entities.Select(MapResponse);
    }

    #endregion
}