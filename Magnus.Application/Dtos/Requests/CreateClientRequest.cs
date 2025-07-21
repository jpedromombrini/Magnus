namespace Magnus.Application.Dtos.Requests;

public record CreateClientRequest(
    string Name,
    string? Email,
    string Document,
    string? Occupation,
    DateOnly? DateOfBirth,
    string RegisterNumber,
    AddressRequest? Address,
    IEnumerable<ClientSocialMediaRequest>? SocialMedias,
    IEnumerable<ClientPhoneRequest>? Phones);