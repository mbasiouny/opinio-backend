namespace Opinio.API.Models.Review;

public class ReviewResponse
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int EntityId { get; set; }
    public DateOnly DateOfTrial { get; set; }
    public string ItemsBought { get; set; }
    public bool? IsConsumer { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<ReviewImageResponse> Images { get; set; } = new List<ReviewImageResponse>();
}

public class ReviewImageResponse
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
}
