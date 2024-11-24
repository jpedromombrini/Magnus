namespace Magnus.Application.Dtos.Requests;

public record UpdateClientRequest(
    string Name,
    string? Email,
    string Document,
    string? Occupation,
    DateOnly? DateOfBirth,
    AddressRequest? Address,
    string? RegisterNumber,
    List<ClientSocialMediaRequest>? SocialMedias,
    List<ClientPhoneRequest>? Phones);