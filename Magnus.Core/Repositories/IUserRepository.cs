using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}