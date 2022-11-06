using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using Shared.Data.Model;

namespace OnePunch.WPF.ViewModel;

public class MonstersViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Monster> Monsters { get; private set; }

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


    public MonstersViewModel()
    {
        Monsters = new ObservableCollection<Monster>();
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


        var monsters = await client.GetFromJsonAsync<IEnumerable<Monster>>("monsters");
        if (monsters != null)
        {
            foreach (var monster in monsters)
            {
                Monsters.Add(monster);
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