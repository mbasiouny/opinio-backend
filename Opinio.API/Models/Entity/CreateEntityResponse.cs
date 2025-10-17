using Opinio.Core.Enums;

namespace Opinio.API.Models.Entity;

public class CreateEntityResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public string Website { get; set; }
    public string Address { get; set; }
    public string ImageUrl { get; set; }
    public EntityStatus Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
