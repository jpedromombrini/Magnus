using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Mappers;

public static class UserMapper
{
    #region Request

    public static User MapToEntity(this CreateUserRequest request)
    {
        return new User(new Email(request.Email), request.Password, request.Name, request.InitialDate,
            request.FinalDate, request.Active, (UserType)request.UserType);
    }
    public static User MapToEntity(this UpdateUserRequest request)
    {
        return new User(new Email(request.Email), request.Password, request.Name, request.InitialDate,
            request.FinalDate, request.Active, (UserType)request.UserType);
    }

    #endregion

    #region Response

    public static UserResponse MapToResponse(this User entity)
    {
        return new UserResponse(entity.Id, entity.Email.Address, (int)entity.UserType, entity.Name, entity.InitialDate,
            entity.FinalDate, entity.Active);
    }

    public static IEnumerable<UserResponse> MapToResponse(this IEnumerable<User> entities)
    {
        return entities.Select(MapToResponse).ToList();
    }

    #endregion
}