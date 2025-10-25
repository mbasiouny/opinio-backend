using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.Core.Repositories;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<PaginatedResult<Review>> ListAsync(int pageNumber, int pageSize, int? entityId, CancellationToken cancellationToken);
    new Task<Review> FindByIdAsync(int id, CancellationToken cancellationToken = default);
}
