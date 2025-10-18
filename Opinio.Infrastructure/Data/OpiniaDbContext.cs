using Microsoft.EntityFrameworkCore;
using Opinio.Core.Entities;
namespace Opinio.Infrastructure.Data;

public class OpiniaDbContext : DbContext
{
    public OpiniaDbContext(DbContextOptions<OpiniaDbContext> options)
        : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<JwtBlacklist> JwtBlacklists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
