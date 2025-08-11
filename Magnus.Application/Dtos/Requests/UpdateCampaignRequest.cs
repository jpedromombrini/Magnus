namespace Magnus.Application.Dtos.Requests;

public record UpdateCampaignRequest(
    string Name,
    string? Description,
    DateOnly InitialDate,
    DateOnly FinalDate,
    bool Active,
    IEnumerable<CampaignItemRequest> Items);