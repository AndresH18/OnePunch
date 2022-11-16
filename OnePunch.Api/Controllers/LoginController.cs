using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePunch.Api.Security.Models;
using OnePunch.Api.Services;
using User = OnePunch.Api.Security.User;

namespace OnePunch.Api.Controllers;

[ApiController]
[Route("login")]
public class LoginController : ControllerBase
{
    private readonly UserManager _userManager;

    public LoginController(UserManager userManager)
    {
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        // verify that user exists in DB
        var user = _userManager.Authenticate(userLogin);
        if (user == null)
            return NotFound("Username or password are invalid.");

        var token = _userManager.GenerateToken(user);
        return Ok(token);
    }


    private User? GetCurrentUser()
    {
        if (HttpContext.User.Identity is ClaimsIdentity identity)
        {
            var userClaims = identity.Claims;
            // creating list to avoid IEnumerable fall
            var claims = userClaims.ToList();
            return new User()
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