using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGF.Models.Entities;

namespace SGF.Models.Data
{
  public class AppDbContext : IdentityDbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
    { }

    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<Category> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Transaction>()
        .Property(t => t.TransactionType)
        .HasConversion<string>();
    }
  }
}