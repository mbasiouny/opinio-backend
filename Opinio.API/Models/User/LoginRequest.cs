using System.ComponentModel.DataAnnotations;

namespace Opinio.API.Models.User;

public class LoginRequest
{
    [Required]
    [MaxLength(200)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
