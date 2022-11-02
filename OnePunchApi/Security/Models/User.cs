using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Security.Models;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; } = string.Empty;

    [Required] public string Username { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;

    public Role Role { get; set; } = Role.Civil;

    public Rank? Rank { get; set; } = null;
}