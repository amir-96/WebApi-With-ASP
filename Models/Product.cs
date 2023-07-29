using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Project.Models
{
  public class Product
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string ImageUrl { get; set; }
    public int Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set;}
  }
}
