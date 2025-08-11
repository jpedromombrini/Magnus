namespace Magnus.Application.Dtos.Responses;

public record CampaignItemResponse(
    Guid Id,
    Guid ProductId,
    ProductResponse Product,
    decimal Price);