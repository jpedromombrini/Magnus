using Magnus.Core.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Magnus.Infrastructure.Identity;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid GetUserId()
    {
        var userIdClaim = httpContextAccessor.HttpContext?
            .User?
            .FindFirst("UserId")?.Value;

        return string.IsNullOrEmpty(userIdClaim) ? Guid.Empty : Guid.Parse(userIdClaim);
    }
}