using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.Core.Services;

public interface ICategoryService
{
    Task<OperationResult<Category>> CreateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<OperationResult<Category>> UpdateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<OperationResult<string>> DeleteCategoryAsync(int id, CancellationToken cancellationToken);
    Task<OperationResult<Category>> GetCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<OperationResult<List<Category>>> ListCategoriesAsync(CancellationToken cancellationToken);
}
