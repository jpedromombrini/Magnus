namespace Magnus.Application.Dtos.Responses;

public record SupplierResponse(
    Guid Id,
    string Name, 
    string Document, 
    string? Phone, 
    string? Email,     
    string? ZipCode,
    string? PublicPlace,
    int Number,
    string? Neighborhood,
    string? City,
    string? State,
    string? Complement);