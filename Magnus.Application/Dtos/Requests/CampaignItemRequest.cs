namespace Magnus.Application.Dtos.Requests;

public record CampaignItemRequest(
    Guid ProductId,
    decimal Price);