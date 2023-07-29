using API_Project.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Project.Models
{
  public class User
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string UserName { get; set; }
    public string HashedPassword { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
  }
}
