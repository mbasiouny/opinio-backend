using Opinio.Core.Entities;

namespace Opinio.Core.Repositories;

public interface IEntityRepository : IGenericRepository<Entity>
{
    Task<List<Entity>> ListByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<bool> IsExistAsync(string name, int categoryId, CancellationToken cancellationToken);
    Task<Entity> GetEntityAsync(int entityId, CancellationToken cancellationToken);
}

