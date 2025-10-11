using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opinio.Core.Entities;

[Table("entities", Schema = "main")]

public class Entity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Required]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }

    [MaxLength(255)]
    [Column("website")]
    public string Website { get; set; }

    [Column("address")]
    public string Address { get; set; }

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

