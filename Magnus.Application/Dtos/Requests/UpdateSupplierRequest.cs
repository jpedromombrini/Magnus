using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record UpdateSupplierRequest(
    string Name, 
    string Document, 
    string Phone, 
    PhoneType PhoneType, 
    string Email,     
    string ZipCode,
    string PublicPlace,
    int Number,
    string Neighborhood,
    string City,
    string State,
    string Complement,
    string RegisterNumber);