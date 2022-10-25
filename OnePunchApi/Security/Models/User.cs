namespace OnePunchApi.Security.Models;

public class User
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string GivenName { get; set; } = default!;
    
    public DateTime DateOfBirth { get; set; }
}