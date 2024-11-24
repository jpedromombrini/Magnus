namespace Magnus.Application.Dtos.Requests;

public record CreateClientRequest(
    string Name,
    string? Email,
    string Document,
    string? Occupation,
    DateOnly? DateOfBirth,
    AddressRequest? Address,
    string? RegisterNumber,
    List<ClientSocialMediaRequest>? SocialMedias,
    List<ClientPhoneRequest>? Phones); 
