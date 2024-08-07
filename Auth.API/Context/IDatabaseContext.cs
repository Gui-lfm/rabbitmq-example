namespace Auth.API.Context;

using Auth.API.Models;
using Microsoft.EntityFrameworkCore;

public interface IDatabaseContext
{

  public DbSet<User> Users { get; set; }
  public int SaveChanges();
}