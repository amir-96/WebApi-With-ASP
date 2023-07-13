using System.ComponentModel.DataAnnotations;

namespace Web_Api_Application.Dtos.Products
{
  public class CreateProductDto
  {
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string? Description { get; set; }
    [Required]
    public int Price { get; set; }
  }
}
