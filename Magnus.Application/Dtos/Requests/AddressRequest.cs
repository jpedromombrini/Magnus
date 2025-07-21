namespace Magnus.Application.Dtos.Requests;

public record AddressRequest(
    string ZipCode,
    string PublicPlace,
    int Number,
    string Neighborhood,
    string City,
    string State,
    string Complement);