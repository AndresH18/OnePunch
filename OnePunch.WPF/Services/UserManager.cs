using System.Diagnostics;
using System.Net.Http.Json;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using OnePunchApi.Security.Models;

namespace OnePunch.WPF.Services;

public class UserManager
{
    public string? Username { get; private set; }
    public string? AuthenticationToken { get; private set; }

    public async Task<bool?> Login(string username, string password)
    {
        var userLogin = CreateUserLogin(username, password);
        var baseAddress = App.Current.Host;
        using var client = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };
        try
        {
            using var response = await client.PostAsync("login", JsonContent.Create(userLogin));

            if (response.IsSuccessStatusCode)
            {
                Username = username;
                AuthenticationToken = await response.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (HttpRequestException requestException)
        {
            Debug.WriteLine(requestException);
            return null;
        }
    }

    public void SignOut()
    {
        AuthenticationToken = null;
        Username = null;
    }

    private UserLogin CreateUserLogin(string username, string password)
    {
        return new UserLogin {Username = username, Password = password};
    }
}