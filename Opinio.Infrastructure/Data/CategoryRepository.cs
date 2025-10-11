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
        existing.UpdatedBy = "Guest";
        existing.UpdatedAt = DateTime.UtcNow;
        base.ApplyMapping(existing, updated);
    }
}
