using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Magnus.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IAuthAppService authAppService) : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<LoginResponse> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        return await authAppService.LoginAsync(request, cancellationToken);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<LoginResponse> RefreshLoginAsync([FromBody] RefreshLoginRequest request,
        CancellationToken cancellationToken)
    {
        return await authAppService.RefreshLoginAsync(request, cancellationToken);
    }
}