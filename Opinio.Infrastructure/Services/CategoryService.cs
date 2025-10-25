using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;
using Opinio.Core.Repositories;
using Opinio.Core.Services;
using Opinio.Infrastructure.Extensions;

namespace Opinio.Infrastructure.Services;

public class CategoryService(
    ICategoryRepository categoryRepository,
    IValidator<Category> validator
    ) : ICategoryService
{
    #region Create
    public async Task<OperationResult<Category>> CreateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(category, cancellationToken);
            if (!validationResult.IsValid)
                return OperationResult<Category>.ValidationError(validationResult.ToErrorDictionary());

            if (await categoryRepository.IsExistAsync(category.Name, cancellationToken))
            {
                return OperationResultHelper.CreateValidationError<Category>(nameof(category.Name), "This Category Name Already Exist");
            }

            category.CreatedBy = "Admin";
            category.CreatedAt = DateTime.UtcNow;

            await categoryRepository.CreateAsync(category, cancellationToken);
            await categoryRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Category>.Success(category, "Category Created Successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<Category>.Failure(message: "Error When Create Category");
        }
    }
    #endregion

    #region Update
    public async Task<OperationResult<Category>> UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(category, cancellationToken);
            if (!validationResult.IsValid)
                return OperationResult<Category>.ValidationError(validationResult.ToErrorDictionary());

            var existingCategory = await categoryRepository.FindAsTrackingAsync(category.Id, cancellationToken);
            if (existingCategory == null)
            {
                return OperationResult<Category>.NotFound("This Category Not Found");
            }

            if (existingCategory.Name != category.Name && (await categoryRepository.IsExistAsync(category.Name, cancellationToken)))
            {
                return OperationResultHelper.CreateValidationError<Category>(nameof(category.Name), "This Category Name Already Exist");
            }

            categoryRepository.Update(existingCategory, category);
            await categoryRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Category>.Success(existingCategory, "Category Updated Successfully");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return OperationResult<Category>.Failure(message: "Concurrency Update For This category");
        }
        catch (Exception ex)
        {
            return OperationResult<Category>.Failure(message: "Error When Update Category");
        }
    }
    #endregion

    #region Delete
    public async Task<OperationResult<string>> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var existingCategory = await categoryRepository.FindAsTrackingAsync(categoryId, cancellationToken);
            if (existingCategory == null)
            {
                return OperationResult<string>.NotFound("This Category Not Found");
            }

            categoryRepository.Delete(existingCategory);
            await categoryRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<string>.Success("This Category Deleted Successfully", "Category Deleted Successfully");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return OperationResult<string>.Failure(message: "Concurrency Delete For This category");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Failure(message: "Error When Delete Category");
        }
    }
    #endregion

    #region GetById
    public async Task<OperationResult<Category>> GetCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var category = await categoryRepository.FindByIdAsync(categoryId, cancellationToken);
            if (category == null)
            {
                return OperationResult<Category>.NotFound("This Category Not Found");
            }

            return OperationResult<Category>.Success(category, "Category By Id");
        }

        catch (Exception ex)
        {
            return OperationResult<Category>.Failure(message: "Error When Get Category By Id");
        }
    }
    #endregion

    #region List
    public async Task<OperationResult<List<Category>>> ListCategoriesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var categories = await categoryRepository.ListAsync(cancellationToken);

            return OperationResult<List<Category>>.Success(categories, "All Categories Listed");
        }

        catch (Exception ex)
        {
            return OperationResult<List<Category>>.Failure(message: "Error When List Categories");
        }
    }
    #endregion
}
