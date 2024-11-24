using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record CreateSellerRequest(
    string Name, 
    string Document, 
    string Phone, 
    string Email,
    string Password,
    PhoneType PhoneType);