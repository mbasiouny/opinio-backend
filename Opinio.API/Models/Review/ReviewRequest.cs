namespace Opinio.API.Models.Review;

public class ReviewRequest
{
    public int EntityId { get; set; }

    public DateOnly DateOfTrial { get; set; }

    public string ItemsBought { get; set; }

    public bool? IsConsumer { get; set; } = true;

    public string ReviewText { get; set; } = string.Empty;

    public int Rating { get; set; }

    public List<ReviewImageRequest> Images { get; set; }
}
