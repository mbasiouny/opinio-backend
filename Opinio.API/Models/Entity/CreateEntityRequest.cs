using System.ComponentModel.DataAnnotations;
using Opinio.Core.Enums;

namespace Opinio.API.Models.Entity;

public class CreateEntityRequest
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [MaxLength(255)]
    public string Website { get; set; }

    public string Address { get; set; }

    [MaxLength(500)]
    public string ImageUrl { get; set; }
    public EntityStatus Status { get; set; }
}
