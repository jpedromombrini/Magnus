using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services;

public interface IUserAppService
{
    Task AddUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
    Task UpdateUserAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<UserResponse>> GetUsersAsync(CancellationToken cancellationToken);
    Task<IEnumerable<UserResponse>> GetUsersByFilterAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
    Task<UserResponse> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
}