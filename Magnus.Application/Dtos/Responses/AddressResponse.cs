namespace Magnus.Application.Dtos.Responses;

public record AddressResponse(
    string ZipCode,
    string PublicPlace,
    int Number,
    string Neighborhood,
    string City,
    string State,
    string Complement);