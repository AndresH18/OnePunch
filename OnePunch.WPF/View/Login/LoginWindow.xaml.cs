using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OnePunchApi.Security.Models;

namespace OnePunch.WPF.View.Login;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window, INotifyPropertyChanged
{
    public TextCommand LoginCommand { get; private set; }

    private string _username = string.Empty;
    private string _password = string.Empty;

    public LoginWindow()
    {
        InitializeComponent();
        Owner = App.Current.MainWindow;
        LoginCommand = new TextCommand(CheckFields, Login);
    }

    private bool CheckFields()
    {
        return (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password));
    }

    private async void Login()
    {
        var userLogin = CreateUserLogin();
        var baseAddress = App.Current.Host;
        var client = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };
        var response = await client.PostAsync("login", JsonContent.Create(userLogin));

        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine($"{response.Content}");
        }
    }

    private UserLogin CreateUserLogin()
    {
        return new UserLogin() {Username = _username, Password = _password};
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        var passBox = (PasswordBox) sender;
        _password = passBox.Password;
        NotifyPropertyChange(nameof(_password));
    }

    private void UsernameBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var usernameBox = (TextBox) sender;
        _username = usernameBox.Text;
        NotifyPropertyChange(nameof(_username));
    }

    private void NotifyPropertyChange(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}

public class TextCommand : ICommand
{
    private readonly Func<bool> _canExecuteFunc;
    private readonly Action _executeFunc;

    public TextCommand(Func<bool> canExecuteFunc, Action executeFunc)
    {
        _canExecuteFunc = canExecuteFunc;
        _executeFunc = executeFunc;
    }

    public bool CanExecute(object? parameter)
    {
        // return _canExecuteFunc.Invoke();
        return false;
    }

    public void Execute(object? parameter)
    {
        _executeFunc.Invoke();
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}