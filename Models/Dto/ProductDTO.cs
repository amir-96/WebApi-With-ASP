using System.ComponentModel.DataAnnotations;

namespace API_Project.Models.Dto
{
  public class ProductDTO
  {
    public int Id { get; set; }
    [Required]
    [MaxLength(40)]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public string Category { get; set; }
    public string ImageUrl { get; set; }
    [Required]
    public int Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
  }
}
