using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class CampaignMapper
{
    #region Request

    public static Campaign MapToEntity(this CreateCampaignRequest request)
    {
        var campaign = new Campaign(request.Name, request.Description, request.InitialDate, request.FinalDate,
            request.Active);
        foreach (var item in request.Items) campaign.AddItem(item.MapToEntity());
        return campaign;
    }

    public static Campaign MapToEntity(this UpdateCampaignRequest request)
    {
        var campaign = new Campaign(request.Name, request.Description, request.InitialDate, request.FinalDate,
            request.Active);
        foreach (var item in request.Items) campaign.AddItem(item.MapToEntity());
        return campaign;
    }

    public static CampaignItem MapToEntity(this CampaignItemRequest request)
    {
        return new CampaignItem(request.ProductId, request.Price);
    }

    public static IEnumerable<CampaignItem> MapToEntity(this IEnumerable<CampaignItemRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion

    #region Response

    public static IEnumerable<CampaignResponse> MapToResponse(this IEnumerable<Campaign> entities)
    {
        return entities.Select(MapToResponse);
    }

    public static CampaignResponse MapToResponse(this Campaign entity)
    {
        return new CampaignResponse(entity.Id, entity.Name, entity.Description, entity.InitialDate, entity.FinalDate,
            entity.Active, entity.Items.MapToResponse());
    }

    public static CampaignItemResponse MapToResponse(this CampaignItem entity)
    {
        return new CampaignItemResponse(entity.Id, entity.ProductId, entity.Product.MapToResponse(), entity.Price);
    }

    public static IEnumerable<CampaignItemResponse> MapToResponse(this IEnumerable<CampaignItem> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}