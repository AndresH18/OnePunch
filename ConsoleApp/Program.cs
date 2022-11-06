// See https://aka.ms/new-console-template for more information

using ConsoleApp;

Console.WriteLine("Hello, World!");

// using var client = new HttpClient{BaseAddress = new Uri("https://localhost:7291")};
//
// var response = await client.GetAsync("MonsterCell")

var hero = (HeroT?) Activator.CreateInstance(typeof(HeroT), "");
Console.WriteLine(hero);

namespace ConsoleApp
{
    internal class HeroT
    {
        public HeroT(string s)
        {
        
        }
    }
}