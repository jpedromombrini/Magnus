namespace Magnus.Application.Dtos.Requests;

public record UpdateClientRequest(
    string Name,
    string? Email,
    string Document,
    string? Occupation,
    DateOnly DateOfBirth,
    AddressRequest? Address,
    string? RegisterNumber,
    IEnumerable<ClientSocialMediaRequest>? SocialMedias,
    IEnumerable<ClientPhoneRequest>? Phones);