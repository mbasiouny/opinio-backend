using Microsoft.EntityFrameworkCore;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;
using Opinio.Core.Repositories;
using Opinio.Core.Services;

namespace Opinio.Infrastructure.Data;

public class ReviewRepository(OpiniaDbContext opiniaDbContext, ICurrentUserService currentUserService) : GenericRepository<Review>(opiniaDbContext), IReviewRepository
{
    protected override void ApplyMapping(Review existing, Review updated)
    {
        existing.DateOfTrial = updated.DateOfTrial;
        existing.ItemsBought = updated.ItemsBought;
        existing.IsConsumer = updated.IsConsumer;
        existing.ReviewText = updated.ReviewText;
        existing.Rating = updated.Rating;
        existing.Images = updated.Images;
        existing.UpdatedBy = currentUserService.Username;
        existing.UpdatedAt = DateTime.UtcNow;
        base.ApplyMapping(existing, updated);
    }

    public async Task<PaginatedResult<Review>> ListAsync(int pageNumber, int pageSize, int? entityId, CancellationToken cancellationToken)
    {
        var query = _dbSet.AsQueryable();

        if (entityId.HasValue)
            query = query.Where(e => e.EntityId == entityId);

        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
            .Include(r => r.Images)
            .OrderBy(e => e.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Review>(totalItems, pageSize, items);
    }
    public new async Task<Review?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsTracking().Include(r => r.Images).FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}
