using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Opinio.Core.Entities;

[Table("review_images", Schema = "main")]
public class ReviewImage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("review_id")]
    public int ReviewId { get; set; }

    [Column("image_url")]
    public string ImageUrl { get; set; } = string.Empty;

    [ForeignKey(nameof(ReviewId))]
    [JsonIgnore]
    public Review Review { get; set; }
}
