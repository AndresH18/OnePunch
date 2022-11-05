// ReSharper disable once CheckNamespace
namespace OnePunchApi.Security.Models;

public class UserLogin
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
}