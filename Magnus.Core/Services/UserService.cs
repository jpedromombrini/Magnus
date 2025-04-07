using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await unitOfWork.Users.GetByIdAsync(id, cancellationToken);
    }
}