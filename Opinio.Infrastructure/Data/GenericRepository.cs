using Microsoft.EntityFrameworkCore;

namespace Opinio.Infrastructure.Data;

public class GenericRepository<TEntity>(OpiniaDbContext context)
    where TEntity : class
{
    protected readonly OpiniaDbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> FindAsTrackingAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
    {
        TEntity val = await _dbSet.FindAsync(id, cancellationToken);
        if (val != null)
        {
            _context.Entry(val).State = EntityState.Unchanged;
        }

        return val;
    }

    public async Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public virtual async Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity existing, TEntity updated)
    {
        ApplyMapping(existing, updated);
        _context.Entry(existing);
    }
    protected virtual void ApplyMapping(TEntity existing, TEntity updated)
    {
    }
    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}