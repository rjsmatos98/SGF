using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGF.Entities;

namespace SGF.Context
{
  public class AppDbContext : IdentityDbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
    { }

    public DbSet<Person> Person { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Person>().HasData(
        new Person
        {
          PersonId = 1,
          Name = "Junior",
          Email = "junior.santos@yahoo.com",
          CPF = "111.111.111-11"
        }
      );
    }
  }
}