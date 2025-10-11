using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.Core.Services;

public interface IEntityService
{
    Task<OperationResult<Entity>> CreateEntityAsync(Entity entity, CancellationToken cancellationToken);
    Task<OperationResult<Entity>> UpdateEntityAsync(Entity entity, CancellationToken cancellationToken);
    Task<OperationResult<string>> DeleteEntityAsync(int id, CancellationToken cancellationToken);
    Task<OperationResult<Entity>> GetEntityAsync(int entityId, CancellationToken cancellationToken);
    Task<OperationResult<List<Entity>>> ListEntitiesByCategoryAsync(int categoryId, CancellationToken cancellationToken);
}
