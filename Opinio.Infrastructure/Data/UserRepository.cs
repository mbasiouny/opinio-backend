using Microsoft.EntityFrameworkCore;
using Opinio.Core.Entities;
using Opinio.Core.Repositories;

namespace Opinio.Infrastructure.Data;

public class UserRepository(OpiniaDbContext opiniaDbContext) : GenericRepository<User>(opiniaDbContext), IUserRepository
{
    public Task<bool> IsExistAsync(string email, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(e => e.Email == email, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
    }

    public async Task RevokeTokenAsync(JwtBlacklist jwtBlacklist, CancellationToken cancellationToken)
    {
        await opiniaDbContext.JwtBlacklists.AddAsync(jwtBlacklist);
    }
}
