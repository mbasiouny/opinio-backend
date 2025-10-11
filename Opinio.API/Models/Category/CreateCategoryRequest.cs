using System.ComponentModel.DataAnnotations;

namespace Opinio.API.Models.Category;

public class CreateCategoryRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public string Description { get; set; }

    [MaxLength(500)]
    public string ImageUrl { get; set; }
}
