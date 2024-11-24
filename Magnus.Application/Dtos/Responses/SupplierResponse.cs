namespace Magnus.Application.Dtos.Responses;

public record SupplierResponse(
    Guid Id,
    string Name, 
    string Document, 
    string Phone, 
    string Email,     
   AddressResponse Address);