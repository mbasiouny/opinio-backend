using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opinio.Core.Entities;

[Table("users", Schema = "main")]

public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("username")]
    public string Username { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("email")]
    public string Email { get; set; }

    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; }

    [Required]
    [Column("salt")]
    public string Salt { get; set; }

    [Required]
    [Column("role")]
    public byte Role { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

}
