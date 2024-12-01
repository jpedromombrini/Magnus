using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record UpdateSellerRequest(
    string Name, 
    string Document, 
    string Phone,
    string Email,
    string Password);
