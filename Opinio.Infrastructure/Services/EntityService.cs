using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;
using Opinio.Core.Repositories;
using Opinio.Core.Services;
using Opinio.Infrastructure.Extensions;

namespace Opinio.Infrastructure.Services;

public class EntityService(
    IEntityRepository entityRepository,
    ICategoryRepository categoryRepository,
    IValidator<Entity> validator
    ) : IEntityService
{
    #region Create
    public async Task<OperationResult<Entity>> CreateEntityAsync(Entity entity, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(entity, cancellationToken);
            if (!validationResult.IsValid)
                return OperationResult<Entity>.ValidationError(validationResult.ToErrorDictionary());

            if (!await categoryRepository.IsExistAsync(entity.CategoryId, cancellationToken))
            {
                return CreateValidationError(nameof(entity.CategoryId), "This Category Not Found");
            }

            entity.CreatedBy = "Guest";
            entity.CreatedAt = DateTime.UtcNow;

            await entityRepository.CreateAsync(entity, cancellationToken);
            await entityRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Entity>.Success(entity, "Entity Created Successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<Entity>.Failure(message: "Error When Create Entity");
        }
    }
    #endregion

    #region Update
    public async Task<OperationResult<Entity>> UpdateEntityAsync(Entity entity, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(entity, cancellationToken);
            if (!validationResult.IsValid)
                return OperationResult<Entity>.ValidationError(validationResult.ToErrorDictionary());

            var existingEntity = await entityRepository.FindAsTrackingAsync(entity.Id, cancellationToken);
            if (existingEntity == null)
            {
                return OperationResult<Entity>.NotFound("This Entity Not Found");
            }

            if (!await categoryRepository.IsExistAsync(entity.CategoryId, cancellationToken))
            {
                return CreateValidationError(nameof(entity.CategoryId), "This Category Not Found");
            }

            entityRepository.Update(existingEntity, entity);
            await entityRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Entity>.Success(existingEntity, "Entity Updated Successfully");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return OperationResult<Entity>.Failure(message: "Concurrency Update For This Entity");
        }
        catch (Exception ex)
        {
            return OperationResult<Entity>.Failure(message: "Error When Update Entity");
        }
    }
    #endregion

    #region Delete
    public async Task<OperationResult<string>> DeleteEntityAsync(int entityId, CancellationToken cancellationToken)
    {
        try
        {
            var existingEntity = await entityRepository.FindAsTrackingAsync(entityId, cancellationToken);
            if (existingEntity == null)
            {
                return OperationResult<string>.NotFound("This Entity Not Found");
            }

            entityRepository.Delete(existingEntity);
            await entityRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<string>.Success("This Entity Deleted Successfully", "Entity Deleted Successfully");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return OperationResult<string>.Failure(message: "Concurrency Delete For This Entity");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Failure(message: "Error When Delete Entity");
        }
    }
    #endregion

    #region GetById
    public async Task<OperationResult<Entity>> GetEntityAsync(int entityId, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await entityRepository.GetEntityAsync(entityId, cancellationToken);
            if (entity == null)
            {
                return OperationResult<Entity>.NotFound("This Entity Not Found");
            }

            return OperationResult<Entity>.Success(entity, "Entity By Id");
        }

        catch (Exception ex)
        {
            return OperationResult<Entity>.Failure(message: "Error When Get Entity By Id");
        }
    }
    #endregion

    #region List
    public async Task<OperationResult<List<Entity>>> ListEntitiesByCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await entityRepository.ListByCategoryIdAsync(categoryId, cancellationToken);

            return OperationResult<List<Entity>>.Success(entities, "All Entities Listed");
        }

        catch (Exception ex)
        {
            return OperationResult<List<Entity>>.Failure(message: "Error When List Entities");
        }
    }
    #endregion

    #region Private
    private OperationResult<Entity> CreateValidationError(string fieldName, string errorMessage)
    {
        return OperationResult<Entity>.ValidationError(
            new Dictionary<string, List<string>>()
            {
                {
                    fieldName,
                    new List<string> { errorMessage }
                },
            }
        );
    }
    #endregion
}
