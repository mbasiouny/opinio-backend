using Microsoft.EntityFrameworkCore;
using Opinio.Core.Entities;
using Opinio.Core.Repositories;

namespace Opinio.Infrastructure.Data;

public class EntityRepository(OpiniaDbContext opiniaDbContext) : GenericRepository<Entity>(opiniaDbContext), IEntityRepository
{
    protected override void ApplyMapping(Entity existing, Entity updated)
    {
        existing.Name = updated.Name;
        existing.Description = updated.Description;
        existing.Website = updated.Website;
        existing.Address = updated.Address;
        existing.ImageUrl = updated.ImageUrl;
        existing.UpdatedBy = "Guest";
        existing.UpdatedAt = DateTime.UtcNow;
        base.ApplyMapping(existing, updated);
    }

    public async Task<List<Entity>> ListByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(x => x.Category)
            .Where(e => e.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> IsExistAsync(string name, int categoryId, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(e => e.Name == name && e.CategoryId == categoryId, cancellationToken);
    }

    public async Task<Entity?> GetEntityAsync(int entityId, CancellationToken cancellationToken)
    {
        return await _dbSet.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == entityId);
    }
}
