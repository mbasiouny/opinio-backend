using Opinio.Core.Entities;
using Opinio.Core.Repositories;

namespace Opinio.Infrastructure.Data;

public class ReviewImageRepository(OpiniaDbContext opiniaDbContext) : GenericRepository<ReviewImage>(opiniaDbContext), IReviewImageRepository
{
    protected override void ApplyMapping(ReviewImage existing, ReviewImage updated)
    {
        base.ApplyMapping(existing, updated);
    }
}
