using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Services;

public class UserAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IUserAppService
{
    public async Task AddUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userDb = await unitOfWork.Users.GetByExpressionAsync(x => x.Name.Equals(request.Name, StringComparison.InvariantCultureIgnoreCase), cancellationToken);
        if (userDb is not null)
            throw new ApplicationException("Já existe um usuário com esse nome");
        userDb = await unitOfWork.Users.GetByExpressionAsync(x => x.Email.Address.ToLower() == request.Email.ToLower(), cancellationToken);
        if (userDb is not null)
            throw new ApplicationException("Já existe um usuário com esse email");
        await unitOfWork.Users.AddAsync(mapper.Map<User>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id, cancellationToken);
        if (user is null)
            throw new EntityNotFoundException(id);
        var email = new Email(request.Email);
        var userType = (UserType)request.UserType;
        user.SetEmail(email);
        user.SetActive(request.Active);
        user.SetName(request.Name);
        user.SetInitialDate(request.InitialDate);
        user.SetFinalDate(request.FinalDate);
        user.SetPassword(request.Password);
        user.SetUserType(userType);
        unitOfWork.Users.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserResponse>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<UserResponse>>(await unitOfWork.Users.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<UserResponse>> GetUsersByFilterAsync(Expression<Func<User, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<UserResponse>>(
            await unitOfWork.Users.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<UserResponse> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id, cancellationToken);
        if (user is null)
            throw new EntityNotFoundException(id);
        return mapper.Map<UserResponse>(user);
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id, cancellationToken);
        if (user is null)
            throw new EntityNotFoundException(id);
        unitOfWork.Users.Delete(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}