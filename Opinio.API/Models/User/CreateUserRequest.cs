using System.ComponentModel.DataAnnotations;

namespace Opinio.API.Models.User;

public class CreateUserRequest
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [MaxLength(200)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
