using System.Security.Claims;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.Core.Services;

public interface IUserService
{
    Task<OperationResult<User>> RegisterAsync(User user, CancellationToken cancellationToken);
    Task<OperationResult<string>> LoginAsync(User user, CancellationToken cancellationToken);
    Task<OperationResult<string>> LogoutAsync(ClaimsPrincipal user, CancellationToken ct);
}
