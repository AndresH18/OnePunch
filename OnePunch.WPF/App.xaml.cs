using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnePunch.WPF.Services;
using OnePunch.WPF.View.Hero;
using OnePunch.WPF.View.Login;
using OnePunch.WPF.View.Monster;

namespace OnePunch.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App) Application.Current;


    public string Host => _configuration["Hosts:remote"];
    public IServiceProvider Services { get; }
    // public IConfiguration Configuration { get; }

    private IConfiguration _configuration;

    public App()
    {
        Services = ConfigureServices();
        _configuration = SetConfiguration();
    }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        /* Add services here */
        var services = new ServiceCollection();

        services.AddSingleton<UserManager>();

        services.AddNavDestinations();

        return services.BuildServiceProvider();
    }

    private static IConfiguration SetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        return builder.Build();
    }

   
}

public static class ServicesExtensions
{
    public static void AddNavDestinations(this ServiceCollection serviceCollection)
    {
        // TODO: add navigation destinations here, maybe as transient
        serviceCollection.AddTransient<LoginWindow>();
        serviceCollection.AddScoped<HeroesPage>();
        serviceCollection.AddScoped<MonstersPage>();
    }
}