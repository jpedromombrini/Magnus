using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Mappers;

public static class ClientPhoneMapper
{
    #region Response

    public static ClientPhoneResponse MapToResponse(this ClientPhone entity)
    {
        return new ClientPhoneResponse(entity.Phone.Number, entity.Description);
    }

    public static IEnumerable<ClientPhoneResponse> MapToResponse(this IEnumerable<ClientPhone> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion

    #region Request

    public static ClientPhone MapToEntity(this ClientPhoneRequest request)
    {
        return new ClientPhone(new Phone(request.Number), request.Description);
    }

    public static IEnumerable<ClientPhone> MapToEntity(this IEnumerable<ClientPhoneRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion
}