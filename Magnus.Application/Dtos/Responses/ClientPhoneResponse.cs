using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record ClientPhoneResponse(
    string Number, 
    string Description);