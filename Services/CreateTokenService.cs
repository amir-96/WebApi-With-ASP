using API_Project.Data;
using API_Project.Interfaces;
using API_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Project.Services
{
  public class CreateTokenService : ICreateToken
  {
    private readonly ApplicationDbContext _context;
    IConfiguration _configuration;
    public CreateTokenService(ApplicationDbContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }

    public async Task<string> CreateToken(string userName, string password)
    {
      try
      {
        var user = await GetUser(userName, password);

        if (user == null)
        {
          throw new Exception($"User not found");
        }

        var keyBytes = Encoding.UTF8.GetBytes(_configuration["JwtOptions:SigningKey"]);
        var symmetricKey = new SymmetricSecurityKey(keyBytes);

        var signingCredentials = new SigningCredentials(
            symmetricKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
      {
        new Claim("Username", user.UserName),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Role, user.Role),
      };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtOptions:Issuer"],
            audience: _configuration["JwtOptions:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials);

        var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
        return rawToken;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    private async Task<User> GetUser(string userName, string password)
    {
      var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);

      if (user == null)
      {
        throw new Exception($"User with username '{userName}' not found.");
      }

      if (!ValidatePassword(password, user.HashedPassword))
      {
        throw new Exception($"Wrong password");
      }

      return user;
    }

    public string HashPassword(string password)
    {
      var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

      return hashedPassword;
    }

    public bool ValidatePassword(string password, string hashedPassword)
    {
      var verified = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

      return verified;
    }
  }
}
