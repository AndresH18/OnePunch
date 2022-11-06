namespace OnePunch.Api.Security;

/// <summary>
/// Esta clase emularia la Base de Datos de los Usuarios
/// </summary>
public static class UserConstants
{
    public static readonly List<User> Users = new()
    {
        new User
        {
            Username = "andres_admin", EmailAddress = "andres.admin@email.com", Password = "AndresAdminPass",
            GivenName = "Andres", Role = "Administrator", Surname = "David",
            DateOfBirth = new DateTime(1999, 6, 28)
        },
        new User
        {
            Username = "david", EmailAddress = "david@email.com", Password = "david_123",
            GivenName = "David", Role = "User", Surname = "David",
            DateOfBirth = new DateTime(2010, 12, 5)
        },
        new User
        {
            Username = "sofi", EmailAddress = "sofi@email.com", Password = "sof_123",
            GivenName = "Sophia", Role = "User", Surname = "sofia",
            DateOfBirth = new DateTime(2000, 2, 10),
        },
        new User
        {
            Username = "sebas", Password = "seb123", Role = "user"
        }
    };
}