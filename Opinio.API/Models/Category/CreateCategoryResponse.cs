namespace Opinio.API.Models.Category;

public class CreateCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
