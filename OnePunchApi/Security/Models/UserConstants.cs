namespace OnePunchApi.Security.Models;

/// <summary>
/// Esta clase emularia la Base de Datos de los Usuarios
/// </summary>
public static class UserConstants
{
    public static List<User> Users = new List<User>
    {
        new()
        {
            Username = "andres_admin", EmailAddress = "andres.admin@email.com", Password = "AndresAdminPass",
            GivenName = "Andres", Role = "Administrator", Surname = "David"
        },
        new()
        {
            Username = "david", EmailAddress = "david@email.com", Password = "david_123",
            GivenName = "David", Role = "User"
        }
    };
}