using Opinio.Core.Entities;
using Opinio.Core.Helpers;

namespace Opinio.Core.Services;

public interface IReviewService
{
    Task<OperationResult<Review>> CreateReviewAsync(Review review, CancellationToken cancellationToken);
    Task<OperationResult<Review>> UpdateReviewAsync(Review review, CancellationToken cancellationToken);
    Task<OperationResult<string>> DeleteReviewAsync(int reviewId, CancellationToken cancellationToken);
    Task<OperationResult<Review>> GetReviewAsync(int reviewId, CancellationToken cancellationToken);
    Task<OperationResult<PaginatedResult<Review>>> ListReviewsAsync(int pageNumber, int pageSize, int? entityId, CancellationToken cancellationToken);
}
