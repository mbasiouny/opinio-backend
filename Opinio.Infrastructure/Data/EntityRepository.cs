using Microsoft.EntityFrameworkCore;
using Opinio.Core.Entities;
using Opinio.Core.Enums;
using Opinio.Core.Helpers;
using Opinio.Core.Repositories;
using Opinio.Core.Services;

namespace Opinio.Infrastructure.Data;

public class EntityRepository(OpiniaDbContext opiniaDbContext, ICurrentUserService currentUserService) : GenericRepository<Entity>(opiniaDbContext), IEntityRepository
{
    protected override void ApplyMapping(Entity existing, Entity updated)
    {
        existing.Name = updated.Name;
        existing.Description = updated.Description;
        existing.Website = updated.Website;
        existing.Address = updated.Address;
        existing.ImageUrl = updated.ImageUrl;
        existing.Status = updated.Status;
        existing.UpdatedBy = currentUserService.Username;
        existing.UpdatedAt = DateTime.UtcNow;
        base.ApplyMapping(existing, updated);
    }

    public async Task<List<Entity>> ListByCategoryIdAsync(int categoryId, int? status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(x => x.Category)
            .Where(e => e.CategoryId == categoryId
                        && (!status.HasValue || e.Status == (EntityStatus)status.Value))
            .ToListAsync(cancellationToken);
    }
    public async Task<Entity?> GetEntityAsync(int entityId, CancellationToken cancellationToken)
    {
        return await _dbSet.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == entityId);
    }
    public async Task<PaginatedResult<Entity>> ListAsync(int pageNumber, int pageSize, int? status, CancellationToken cancellationToken)
    {
        var query = _dbSet.AsQueryable();

        if (status.HasValue)
            query = query.Where(e => e.Status == (EntityStatus)status.Value);

        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(e => e.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Entity>(totalItems, pageSize, items);
    }

    public Task<bool> IsExistAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
    }
}
