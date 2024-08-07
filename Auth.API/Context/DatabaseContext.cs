namespace Auth.API.Context;
using Microsoft.EntityFrameworkCore;
using Auth.API.Models;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=localhost;Database=ShopTrybe;User=SA;Password=ShopTrybe123!;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

}