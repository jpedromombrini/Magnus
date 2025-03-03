using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Magnus.Application.Services.Interfaces;

public interface IAuthAppService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    Task<LoginResponse> RefreshLoginAsync(RefreshLoginRequest request, CancellationToken cancellationToken);
}