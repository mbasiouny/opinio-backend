using Opinio.Core.Entities;

namespace Opinio.Core.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> IsExistAsync(string email, CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task RevokeTokenAsync(JwtBlacklist jwtBlacklist, CancellationToken cancellationToken);
}

