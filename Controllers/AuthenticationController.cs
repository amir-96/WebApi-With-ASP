using API_Project.Models;
using API_Project.Models.Dto;
using API_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Project.Controllers
{
  [Route("api/auth")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    private readonly CreateTokenService _token;
    public AuthenticationController(CreateTokenService token)
    {
      _token = token;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticationBodyDTO AuthenticationBody)
    {
      try
      {
        var Token = await _token.CreateToken(AuthenticationBody.UserName, AuthenticationBody.Password);

        return Ok(Token);
      }
      catch(Exception ex)
      {
        return Unauthorized(ex.Message);
      }
    }
  }
}
