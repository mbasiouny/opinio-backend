namespace Opinio.Core.Services;

public interface ICurrentUserService
{
    string UserId { get; }
    string Username { get; }
    string Email { get; }
    string Role { get; }
    bool IsAuthenticated { get; }
}
