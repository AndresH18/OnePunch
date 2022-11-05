using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.Data.Model;

namespace OnePunch.WPF.ViewModel;

public class HeroesViewModel
{
    public Hero? SelectedHero { get; set; }
    public ObservableCollection<Hero> Heroes { get; set; }

    public HeroesViewModel()
    {
        Heroes = new ObservableCollection<Hero>();
        LoadData();
    }

    private async void LoadData()
    {
        // TODO: get data from back
        var client = new HttpClient()
        {
            BaseAddress = new Uri("https://onepunchapi.azurewebsites.net")
        };
        var heroes = await client.GetFromJsonAsync<IEnumerable<Hero>>("heroes");
        if (heroes != null)
        {
            foreach (var hero in heroes)
            {
                Heroes.Add(hero);
            }
        }
    }
}