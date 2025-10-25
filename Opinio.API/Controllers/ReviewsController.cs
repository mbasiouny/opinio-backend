using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Opinio.API.Models.Review;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;
using Opinio.Core.Services;

namespace Opinio.API.Controllers;

[ApiController]
[Route("api/")]
public class ReviewsController(IReviewService reviewService, IMapper mapper) : ControllerBase
{
    #region POST - Create
    [HttpPost("reviews")]
    public async Task<IActionResult> CreateReview([FromBody] ReviewRequest request, CancellationToken cancellationToken)
    {
        var review = mapper.Map<Review>(request);
        var createdReview = await reviewService.CreateReviewAsync(review, cancellationToken);

        var reviewResponse = mapper.Map<OperationResult<ReviewResponse>>(createdReview);
        return Ok(reviewResponse);
    }
    #endregion
    #region Put - Update
    [HttpPut("reviews/{id}")]
    public async Task<IActionResult> UpdateReview([FromRoute] int id, [FromBody] ReviewRequest request, CancellationToken cancellationToken)
    {
        var review = mapper.Map<Review>(request);

        review.Id = id;
        var updatedReview = await reviewService.UpdateReviewAsync(review, cancellationToken);

        var reviewResponse = mapper.Map<OperationResult<ReviewResponse>>(updatedReview);
        return Ok(reviewResponse);
    }
    #endregion

    #region Delete - Delete
    [HttpDelete("reviews/{id}")]
    public async Task<IActionResult> DeleteReview([FromRoute] int id, CancellationToken cancellationToken)
    {
        var message = await reviewService.DeleteReviewAsync(id, cancellationToken);

        return Ok(message);
    }
    #endregion
    #region Get - GetById

    [HttpGet("reviews/{id}")]
    public async Task<IActionResult> GetReview([FromRoute] int id, CancellationToken cancellationToken)
    {
        var review = await reviewService.GetReviewAsync(id, cancellationToken);

        var reviewResponse = mapper.Map<OperationResult<GetReviewResponse>>(review);

        return Ok(reviewResponse);
    }
    #endregion

    #region Get - List

    [HttpGet("reviews")]
    public async Task<IActionResult> ListReviews([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] int? entityId, CancellationToken cancellationToken = default)
    {
        var reviews = await reviewService.ListReviewsAsync(pageNumber ?? 1, pageSize ?? 10, entityId, cancellationToken);
        var reviewResponse = mapper.Map<OperationResult<PaginatedResult<GetReviewResponse>>>(reviews);

        return Ok(reviewResponse);
    }
    #endregion
}
