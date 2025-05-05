using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record CreateUserRequest(
    string Email,
    string Password,
    int UserType,
    string Name,
    DateTime InitialDate,
    DateTime FinalDate,
    bool Active);