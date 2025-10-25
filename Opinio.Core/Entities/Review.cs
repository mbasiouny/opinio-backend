using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opinio.Core.Entities;

[Table("reviews", Schema = "main")]
public class Review
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Required]
    [Column("entity_id")]
    public int EntityId { get; set; }

    [Required]
    [Column("date_of_trial")]
    public DateOnly DateOfTrial { get; set; }

    [Column("items_bought")]
    public string ItemsBought { get; set; }

    [Column("is_consumer")]
    public bool? IsConsumer { get; set; }

    [Required]
    [Column("review_text")]
    public string ReviewText { get; set; } = string.Empty;

    [Required]
    [Range(1, 5)]
    [Column("rating")]
    public int Rating { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("created_by")]
    public string CreatedBy { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [MaxLength(200)]
    [Column("updated_by")]
    public string UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    public ICollection<ReviewImage> Images { get; set; } = new List<ReviewImage>();
}
