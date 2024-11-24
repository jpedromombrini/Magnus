namespace Magnus.Application.Dtos.Requests;

public record UpdateUserRequest(
    string Email, 
    string Password,
    int UserType,
    string Name,
    DateTime InitialDate,
    DateTime FinalDate,
    bool Active);