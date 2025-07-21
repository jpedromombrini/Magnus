using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class ClientSocialMediaMapper
{
    #region Request

    public static ClientSocialMedia MapToEntity(this ClientSocialMediaRequest request)
    {
        return new ClientSocialMedia(request.Name, request.Link);
    }

    public static IEnumerable<ClientSocialMedia> MapToEntity(this IEnumerable<ClientSocialMediaRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion

    #region Response

    public static ClientSocialMediaResponse MapToResponse(this ClientSocialMedia entity)
    {
        return new ClientSocialMediaResponse(entity.Name, entity.Link);
    }

    public static IEnumerable<ClientSocialMediaResponse> MapToResponse(this IEnumerable<ClientSocialMedia> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}