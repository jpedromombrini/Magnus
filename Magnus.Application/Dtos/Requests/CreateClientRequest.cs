namespace Magnus.Application.Dtos.Requests;

public record CreateClientRequest(
    string Name,
    string? Email,
    string Document,
    string? Occupation,
    DateOnly DateOfBirth,
    string? ZipCode,
    string? PublicPlace,
    int Number,
    string? Neighborhood,
    string? City,
    string? State,
    string? Complement,
    string? RegisterNumber,
    List<ClientSocialMediaRequest>? SocialMedias,
    List<ClientPhoneRequest>? Phones); 
