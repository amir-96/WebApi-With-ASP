using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Project.Models.Dto
{
  public class AuthenticationBodyDTO
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}
