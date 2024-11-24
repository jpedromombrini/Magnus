using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserAppService userAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return await userAppService.GetUsersAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<UserResponse>> GetUsersByFilterAsync([FromQuery] string filter,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filter))
            return [];
        return await userAppService.GetUsersByFilterAsync(x => x.Name.ToLower().Contains(filter.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<UserResponse> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await userAppService.GetUserByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddUserAsync([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        await userAppService.AddUserAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateUserAsync(Guid id, [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        await userAppService.UpdateUserAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        await userAppService.DeleteUserAsync(id, cancellationToken);
    }
}