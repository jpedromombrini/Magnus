namespace Magnus.Application.Dtos.Responses;

public record CampaignResponse(
    Guid Id,
    string Name,
    string? Description,
    DateOnly InitialDate,
    DateOnly FinalDate,
    bool Active,
    IEnumerable<CampaignItemResponse> Items);