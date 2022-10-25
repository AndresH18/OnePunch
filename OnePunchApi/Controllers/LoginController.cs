using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnePunchApi.Security.Models;

namespace OnePunchApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private IConfiguration _config;

    public LoginController(IConfiguration config)
    {
        _config = config;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        // verify that user exists in DB
        var user = Authenticate(userLogin);
        if (user == null)
            return NotFound("Username or password are invalid.");

        var token = Generate(user);
        return Ok(token);
    }

    private User? Authenticate(UserLogin userLogin)
    {
        // search for the user in the DB.
        var user = UserConstants.Users.FirstOrDefault(u =>
            u.Username.Equals(userLogin.Username, StringComparison.OrdinalIgnoreCase) &&
            u.Password.Equals(userLogin.Password));

        return user ?? null;
    }

    private string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimTypes.GivenName, user.GivenName),
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private User? GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var userClaims = identity.Claims;
            // creating list to avoid IEnumerable fall
            var claims = userClaims.ToList();
            return new User
            {
                Username = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!,
                EmailAddress = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!,
                GivenName = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value!,
                Surname = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value!,
                Role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value!,
            };
        }

        return null;
    }
}