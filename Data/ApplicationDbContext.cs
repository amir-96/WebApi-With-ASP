using API_Project.Models;
using API_Project.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace API_Project.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().HasData(
          new User()
          {
            Id = 1,
            UserName = "amir",
            HashedPassword = BCrypt.Net.BCrypt.HashPassword("test"),
            Email = "test@test.com",
            Role = "Admin",
            Name = "amir",
            PhoneNumber = "09122222222",
          }
      );

      modelBuilder.Entity<Product>().HasData(
          new Product()
          {
            Id = 1,
            Name = "Laptop",
            Description = "Test",
            Category = "Test",
            ImageUrl = "Test",
            Price = 200000,
            CreatedDate = DateTime.Now.ToUniversalTime(),
          }
      );
    }
  }
}
