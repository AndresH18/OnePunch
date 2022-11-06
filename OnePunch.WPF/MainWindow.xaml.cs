using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using OnePunch.Api.Security.Models;
using OnePunch.WPF.Services;
using OnePunch.WPF.View.Hero;
using OnePunch.WPF.View.Login;
using OnePunch.WPF.View.Monster;
using Shared.Data.Model;

namespace OnePunch.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public Visibility AdminVisibility
    {
        get => _adminVisibility;
        private set
        {
            _adminVisibility = value;
            NotifyPropertyChanged(nameof(AdminVisibility));
        }
    }

    public Visibility HeroSVisibility
    {
        get => _heroSVisibility;
        private set
        {
            _heroSVisibility = value;
            NotifyPropertyChanged(nameof(HeroSVisibility));
        }
    }

    public Visibility SaitamaVisibility
    {
        get => _saitamaVisibility;
        private set
        {
            _saitamaVisibility = value;
            NotifyPropertyChanged(nameof(SaitamaVisibility));
        }
    }

    private Visibility _adminVisibility = Visibility.Visible;
    private Visibility _heroSVisibility = Visibility.Visible;
    private Visibility _saitamaVisibility = Visibility.Visible;

    private readonly UserManager _userManager;

    private readonly IServiceProvider _services;

    private readonly Dictionary<string, Type> _navigationDictionary = new()
    {
        {"view-heroes", typeof(HeroesPage)},
        {"view-monsters", typeof(MonstersPage)}
    };

    private Type? _selectedType = default;

    public MainWindow()
    {
        InitializeComponent();
        _services = App.Current.Services;

        _userManager = _services.GetRequiredService<UserManager>();
    }

    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem {Tag: string tag})
        {
            if (_navigationDictionary.TryGetValue(tag, out var navType))
            {
                if (_selectedType != navType)
                {
                    _selectedType = navType;
                    var o = _services.GetService(_selectedType);
                    Frame.Navigate(o);
                }
            }
            else
            {
                Debug.WriteLine($"tag not found: {tag}");
            }
        }
    }

    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        var loginWindow = _services.GetRequiredService<LoginWindow>();

        var result = loginWindow.ShowDialog();

        if (result == true)
        {
            AccountMenuItem.Header = FormatUnderscore(_userManager.Username!);
            LoginMenuItem.Visibility = Visibility.Collapsed;
            LogoutMenuItem.Visibility = Visibility.Visible;

            /* // changes visibility of items based on credentials
            switch (_userManager.Role)
            {
                case Role.Hero:
                    HeroSVisibility = _userManager.Rank == Rank.S ? Visibility.Visible : Visibility.Collapsed;
                    break;
                case Role.Admin:
                    AdminVisibility = Visibility.Visible;
                    break;
                case Role.Civil:
                default:
                    AdminVisibility = Visibility.Collapsed;
                    HeroSVisibility = Visibility.Collapsed;
                    break;
            }

            SaitamaVisibility =
                _userManager.Username!.Equals("saitama") ? Visibility.Visible : Visibility.Collapsed;
            */
        }
    }

    private void LogoutMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        _userManager.SignOut();

        AccountMenuItem.Header = "Account";
        LoginMenuItem.Visibility = Visibility.Visible;
        LogoutMenuItem.Visibility = Visibility.Collapsed;

        /* // changes visibility of items based on credentials
        AdminVisibility = Visibility.Collapsed;
        HeroSVisibility = Visibility.Collapsed;
        SaitamaVisibility = Visibility.Collapsed;
        */
    }

    // TODO: create events for login and logout

    private void NotifyPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private static string FormatUnderscore(string s)
    {
        return s.Replace("_", "__");
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}