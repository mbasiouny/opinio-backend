namespace Opinio.Core.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> FindAsTrackingAsync(int id, CancellationToken cancellationToken = default);
    Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity existing, TEntity updated);
    void Delete(TEntity entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
