using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record UpdateSupplierRequest(
    string Name, 
    string Document, 
    string Phone, 
    PhoneType PhoneType, 
    string Email,     
    AddressRequest Address);