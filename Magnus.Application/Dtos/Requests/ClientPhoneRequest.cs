using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record ClientPhoneRequest(
    string Number, 
    string Description,
    PhoneType PhoneType);