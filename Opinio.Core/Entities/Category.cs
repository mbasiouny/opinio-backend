using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opinio.Core.Entities;

[Table("categories", Schema = "main")]

public class Category
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [MaxLength(500)]
    [Column("image_url")]
    public string ImageUrl { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("created_by")]
    public string CreatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [MaxLength(200)]
    [Column("updated_by")]
    public string UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
