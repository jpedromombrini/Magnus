using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record CreateSupplierRequest(
    string Name, 
    string Document, 
    string Phone, 
    string Email,    
    string ZipCode,
    string PublicPlace,
    int Number,
    string Neighborhood,
    string City,
    string State,
    string Complement);