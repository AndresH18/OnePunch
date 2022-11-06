using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OnePunch.WPF.Services;
using OnePunchApi.Security.Models;

namespace OnePunch.WPF.View.Login;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    private readonly UserManager _userManager;
    private string _username = string.Empty;
    private string _password = string.Empty;

    public LoginWindow(UserManager userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        
        InitializeComponent();

        ProgressBar.Width = LoginButton.Width;
        Owner = App.Current.MainWindow;
    }

    private void CheckFields()
    {
        var valid = !(string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password));
        LoginButton.IsEnabled = valid;
    }

    private async Task Login()
    {
        var result = await _userManager.Login(_username, _password);
        if (result == true)
        {
            DialogResult = true;
        }
        else
        {
            // TODO: verify result. false: bad credentials, null: login failed
        }
    }


    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        var passBox = (PasswordBox) sender;
        _password = passBox.Password;
        CheckFields();
    }

    private void UsernameBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var usernameBox = (TextBox) sender;
        _username = usernameBox.Text;

        CheckFields();
    }


    private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        ProgressBar.Visibility = Visibility.Visible;
        LoginButton.IsEnabled = false;
        await Login();

        LoginButton.IsEnabled = true;
        ProgressBar.Visibility = Visibility.Hidden;
    }

    private void TextField_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (ReferenceEquals(sender, UsernameBox))
            {
                PasswordBox.Focus();
            }
            else if (ReferenceEquals(sender, PasswordBox) && LoginButton.IsEnabled)
            {
                LoginButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }

    private void PasswordBox_OnGotFocus(object sender, RoutedEventArgs e)
    {
        PasswordBox.SelectAll();
    }
}