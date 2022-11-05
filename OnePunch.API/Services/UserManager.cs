using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OnePunch.Api.Data;
using OnePunch.Api.Security.Models;
using Shared.Data.Model;

namespace OnePunch.Api.Services;

public class UserManager
{
    private const string RankClaim = "rank";

    private readonly ILogger<UserManager> _logger;
    private readonly HeroAssociationDb _db;
    private readonly IConfiguration _config;

    public UserManager(ILogger<UserManager> logger, HeroAssociationDb db, IConfiguration config)
    {
        _logger = logger;
        _db = db;
        _config = config;
    }

    public User? Authenticate(UserLogin login)
    {
        var user = _db.Users
            .FirstOrDefault(u => u.Username.Equals(login.Username)
                                 && u.Password.Equals(login.Password));
        _logger.LogInformation($"Authenticated user: {login.Username}");
        return user;
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            // add claims
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(RankClaim, user.Rank.ToString()!)
        };


        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private User? GetCurrentUser(HttpContext context)
    {
        if (context.User.Identity is ClaimsIdentity identity)
        {
            var claims = identity.Claims;
        }

        return null;
    }

    public User? GetCurrentUser(AuthorizationHandlerContext context)
    {
        var userClaims = context.User.Claims.ToList();
        try
        {
            var username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var roleString = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var rankString = userClaims.FirstOrDefault(c => c.Type == RankClaim)?.Value;

            Enum.TryParse<Rank>(userClaims.FirstOrDefault(c => c.Type == RankClaim)?.Value, out var rank);

            if (!string.IsNullOrWhiteSpace(username) && Enum.TryParse<Role>(roleString, out var role))
            {
                return new User
                {
                    Username = username,
                    Role = role,
                    Rank = rank,
                };
            }
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Failed to parse enum.");
        }

        return default;
    }
}