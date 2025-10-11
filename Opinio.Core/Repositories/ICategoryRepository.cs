using Opinio.Core.Entities;

namespace Opinio.Core.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> IsExistAsync(string name, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(int id, CancellationToken cancellationToken);
}
