namespace Magnus.Application.Dtos.Responses;

public record ClientResponse(
    Guid Id,
    string Name,
    string? Email,
    string Document,
    string? Occupation,
    DateOnly? DateOfBirth,
    string? ZipCode,
    string? PublicPlace,
    int Number,
    string? Neighborhood,
    string? City,
    string? State,
    string? Complement,
    string? RegisterNumber,
    List<ClientSocialMediaResponse>? SocialMedias,
    List<ClientPhoneResponse>? Phones);