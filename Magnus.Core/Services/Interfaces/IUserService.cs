using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
}