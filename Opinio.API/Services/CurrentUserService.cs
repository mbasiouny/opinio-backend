using System.Security.Claims;
using Opinio.Core.Services;

namespace Opinio.API.Services;

public class CurrentUserService : ICurrentUserService
{
    public string UserId { get; }
    public string Username { get; }
    public string Email { get; }
    public string Role { get; }
    public bool IsAuthenticated { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated ?? false)
        {
            IsAuthenticated = true;

            UserId = user.FindFirst("userId")?.Value;
            Username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? user.FindFirst("sub")?.Value;
            Email = user.FindFirst(ClaimTypes.Email)?.Value;
            Role = user.FindFirst(ClaimTypes.Role)?.Value;
        }
        else
        {
            IsAuthenticated = false;
            UserId = null;
            Username = "Guest";
            Email = null;
            Role = null;
        }
    }
}
