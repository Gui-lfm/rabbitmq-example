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
            var server = Environment.GetEnvironmentVariable("DBSERVER");
            var database = Environment.GetEnvironmentVariable("DBNAME");
            var dbuser = Environment.GetEnvironmentVariable("DBUSER");
            var dbpass = Environment.GetEnvironmentVariable("DBPASSWORD");
            var connectionString = "Server="+server+";Database="+database+";User="+dbuser+";Password="+dbpass+";TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

}