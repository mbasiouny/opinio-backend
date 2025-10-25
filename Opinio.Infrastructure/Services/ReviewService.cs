using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;
using Opinio.Core.Repositories;
using Opinio.Core.Services;
using Opinio.Infrastructure.Extensions;

namespace Opinio.Infrastructure.Services;

public class ReviewService(
    IReviewRepository reviewRepository,
    IEntityRepository entityRepository,
    ICurrentUserService currentUserService,
    IValidator<Review> validator
    ) : IReviewService
{
    #region Create
    public async Task<OperationResult<Review>> CreateReviewAsync(Review review, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(review, cancellationToken);
            if (!validationResult.IsValid)
                return OperationResult<Review>.ValidationError(validationResult.ToErrorDictionary());

            if (!await entityRepository.IsExistAsync(review.EntityId, cancellationToken))
            {
                return OperationResult<Review>.NotFound("This Entity Not Found");
            }

            if (int.TryParse(currentUserService.UserId, out var userId))
                review.UserId = userId;

            review.CreatedBy = currentUserService.Username;
            review.CreatedAt = DateTime.UtcNow;

            await reviewRepository.CreateAsync(review, cancellationToken);
            await reviewRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Review>.Success(review, "Review Created Successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<Review>.Failure(message: "Error When Create Review");
        }
    }
    #endregion

    #region Update
    public async Task<OperationResult<Review>> UpdateReviewAsync(Review review, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(review, cancellationToken);
            if (!validationResult.IsValid)
                return OperationResult<Review>.ValidationError(validationResult.ToErrorDictionary());

            var existingReview = await reviewRepository.FindByIdAsync(review.Id, cancellationToken);
            if (existingReview == null)
            {
                return OperationResult<Review>.NotFound("This Review Not Found");
            }

            reviewRepository.Update(existingReview, review);
            await reviewRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Review>.Success(existingReview, "Review Updated Successfully");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return OperationResult<Review>.Failure(message: "Concurrency Update For This Review");
        }
        catch (Exception ex)
        {
            return OperationResult<Review>.Failure(message: "Error When Update Review");
        }
    }
    #endregion

    #region Delete
    public async Task<OperationResult<string>> DeleteReviewAsync(int reviewId, CancellationToken cancellationToken)
    {
        try
        {
            var existingReview = await reviewRepository.FindAsTrackingAsync(reviewId, cancellationToken);
            if (existingReview == null)
            {
                return OperationResult<string>.NotFound("This Review Not Found");
            }

            reviewRepository.Delete(existingReview);
            await reviewRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<string>.Success("This Review Deleted Successfully", "Review Deleted Successfully");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return OperationResult<string>.Failure(message: "Concurrency Delete For This Review");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Failure(message: "Error When Delete Review");
        }
    }
    #endregion

    #region GetById
    public async Task<OperationResult<Review>> GetReviewAsync(int reviewId, CancellationToken cancellationToken)
    {
        try
        {
            var review = await reviewRepository.FindByIdAsync(reviewId, cancellationToken);
            if (review == null)
            {
                return OperationResult<Review>.NotFound("This Review Not Found");
            }

            return OperationResult<Review>.Success(review, "Review By Id");
        }

        catch (Exception ex)
        {
            return OperationResult<Review>.Failure(message: "Error When Get Review By Id");
        }
    }
    #endregion

    #region ListPaginated
    public async Task<OperationResult<PaginatedResult<Review>>> ListReviewsAsync(int pageNumber, int pageSize, int? entityId, CancellationToken cancellationToken)
    {
        try
        {
            if (pageNumber <= 0)
            {
                return OperationResultHelper.CreateValidationError<PaginatedResult<Review>>(nameof(pageNumber), "Invalid Page Number");
            }
            if (pageSize <= 0)
            {
                return OperationResultHelper.CreateValidationError<PaginatedResult<Review>>(nameof(pageSize), "Invalid Page Size");
            }
            var paginatedResult = await reviewRepository.ListAsync(pageNumber, pageSize, entityId, cancellationToken);
            return OperationResult<PaginatedResult<Review>>.Success(paginatedResult);
        }
        catch (Exception ex)
        {
            return OperationResult<PaginatedResult<Review>>.Failure(message: "Error When List Reviews Paginated");
        }
    }
    #endregion
}
