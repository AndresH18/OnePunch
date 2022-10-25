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
            // new Claim(ClaimTypes.Email, user.EmailAddress),
            // new Claim(ClaimTypes.GivenName, user.GivenName),
            // new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Role, user.Role),
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString())
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
        if (HttpContext.User.Identity is ClaimsIdentity identity)
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
                DateOfBirth = DateTime.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)?.Value!)
            };
        }

        return null;
    }
}