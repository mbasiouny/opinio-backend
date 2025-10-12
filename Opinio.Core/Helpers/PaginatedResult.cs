namespace Opinio.Core.Helpers;

public class PaginatedResult<TEntity>
{
    public int TotalItems { get; }
    public int PageSize { get; }
    public IEnumerable<TEntity> Items { get; }

    public PaginatedResult(int totalItems, int pageSize, IEnumerable<TEntity> items)
    {
        TotalItems = totalItems;
        PageSize = pageSize;
        Items = items;
    }
}
