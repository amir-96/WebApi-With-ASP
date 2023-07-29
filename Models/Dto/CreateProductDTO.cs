using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace API_Project.Models.Dto
{
  public class CreateProductDTO
  {
    [Required]
    [MaxLength(40)]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public string Category { get; set; }
    public string ImageUrl { get; set; }
    [Required]
    public int Price { get; set; }
  }
}
