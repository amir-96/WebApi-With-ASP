using API_Project.Models;

namespace API_Project.Interfaces
{
  public interface ICreateToken
  {
    Task<string> CreateToken(string userName, string password);
    string HashPassword(string password);
    bool ValidatePassword(string password, string hashedPassword);
  }
}
