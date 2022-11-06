using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using Shared.Data.Model;

namespace OnePunch.WPF.ViewModel;

public class HeroesViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Hero> Heroes { get; set; }

    public Visibility IsLoadingVisibility
    {
        get => _isLoadingVisibility;
        private set
        {
            _isLoadingVisibility = value;
            NotifyPropertyChanged(nameof(IsLoadingVisibility));
        }
    }

    private Visibility _isLoadingVisibility;

    public HeroesViewModel()
    {
        Heroes = new ObservableCollection<Hero>();
        LoadData();
    }

    public void Refresh()
    {
        Heroes.Clear();
        LoadData();
    }

    private async void LoadData()
    {
        IsLoadingVisibility = Visibility.Visible;

        HttpClient client;
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://onepunchapi.azurewebsites.net")
            };
        }
        else
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(App.Current.Host)
            };
        }

        var heroes = await client.GetFromJsonAsync<IEnumerable<Hero>>("heroes");
        if (heroes != null)
        {
            foreach (var hero in heroes)
            {
                Heroes.Add(hero);
            }
        }

        IsLoadingVisibility = Visibility.Collapsed;
    }


    private void NotifyPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}