using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opinio.Core.Entities;

[Table("jwt_blacklist", Schema = "main")]
public class JwtBlacklist
{
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("jti")]
    public string Jti { get; set; }

    [Required]
    [Column("revoked_at")]
    public DateTime RevokedAt { get; set; }

    [Required]
    [Column("expires_at")]
    public DateTime ExpiresAt { get; set; }
}
