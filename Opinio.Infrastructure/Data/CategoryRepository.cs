using Microsoft.EntityFrameworkCore;
using Opinio.Core.Entities;
using Opinio.Core.Repositories;

namespace Opinio.Infrastructure.Data;

public class CategoryRepository(OpiniaDbContext opiniaDbContext) : GenericRepository<Category>(opiniaDbContext), ICategoryRepository
{
    protected override void ApplyMapping(Category existing, Category updated)
    {
        existing.Name = updated.Name;
        existing.Description = updated.Description;
        existing.ImageUrl = updated.ImageUrl;
        existing.UpdatedBy = "Admin";
        existing.UpdatedAt = DateTime.UtcNow;
        base.ApplyMapping(existing, updated);
    }

    public Task<bool> IsExistAsync(string name, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(e => e.Name == name, cancellationToken);
    }

    public Task<bool> IsExistAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
    }
}
