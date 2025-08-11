namespace Magnus.Application.Dtos.Requests;

public record CreateCampaignRequest(
    string Name,
    string? Description,
    DateOnly InitialDate,
    DateOnly FinalDate,
    bool Active,
    IEnumerable<CampaignItemRequest> Items);