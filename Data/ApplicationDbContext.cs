using API_Project.Models.Dto;

namespace API_Project.Data
{
  public class ApplicationDbContext
  {
    public static List<ProductDTO> ProductsList = new List<ProductDTO>
      {
        new ProductDTO { Id = 1, Name = "Laptop", Description = "Test" },
        new ProductDTO { Id = 2, Name = "Mobile", Description = "Test" }
      };
  }
}
