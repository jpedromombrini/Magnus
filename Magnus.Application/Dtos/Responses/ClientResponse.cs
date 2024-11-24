namespace Magnus.Application.Dtos.Responses;

public record ClientResponse(
    Guid Id,
    string Name,
    string? Email,
    string Document,
    string? Occupation,
    DateOnly? DateOfBirth,
    AddressResponse? Address,
    string? RegisterNumber,
    List<ClientSocialMediaResponse>? SocialMedias,
    List<ClientPhoneResponse>? Phones);