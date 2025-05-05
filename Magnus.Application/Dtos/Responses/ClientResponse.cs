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
    IEnumerable<ClientSocialMediaResponse>? SocialMedias,
    IEnumerable<ClientPhoneResponse>? Phones);