using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.Core.Repositories;

public interface IEntityRepository : IGenericRepository<Entity>
{
    Task<List<Entity>> ListByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<Entity> GetEntityAsync(int entityId, CancellationToken cancellationToken);
    Task<PaginatedResult<Entity>> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}

