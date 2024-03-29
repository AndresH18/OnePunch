﻿using OnePunch.Api.Security.Models;
using Shared.Data.Model;

namespace OnePunch.Api.Data;

public static class SeedData
{
    public static void Initialize(HeroAssociationDb db)
    {
        // https://onepunchman.fandom.com/wiki/Heroes
        var heroes = new Hero[]
        {
            new() {Name = "Saitama", Rank = Rank.C},
            new() {Name = "Genos", Rank = Rank.S},
        };
        // https://onepunchman.fandom.com/wiki/Mysterious_Beings#Demon
        var monsters = new Monster[]
        {
            new() {Name = "Garou", DisasterLevel = DisasterLevel.Dragon},
            new() {Name = "Nyan", DisasterLevel = DisasterLevel.Dragon},
            new() {Name = "Boros", DisasterLevel = DisasterLevel.Dragon},
            new() {Name = "Beast King", DisasterLevel = DisasterLevel.Demon},
            new() {Name = "Gouketsu", DisasterLevel = DisasterLevel.Dragon},
        };
        var monsterCells = new MonsterCell[]
        {
            new() {IsConsumed = true, Monster = monsters[1]},
            new() {IsConsumed = true, Monster = monsters[4]},
        };

        var sponsor = new Sponsor
        {
            Name = "Narinki",
            Heroes = {heroes[1]},
            Monsters = {monsters[1]}
        };

        var fights = new Fight[]
        {
            new Fight {Id = 1, Hero = heroes[0], Monster = monsters[2], IsHeroWinner = true},
        };

        db.Heroes.AddRange(heroes);
        db.Monsters.AddRange(monsters);
        db.Sponsors.Add(sponsor);
        db.Fights.AddRange(fights);
        db.MonsterCells.AddRange(monsterCells);

        AddUsers(db);

        db.SaveChanges();
    }

    private static void AddUsers(HeroAssociationDb db)
    {
        db.Users.AddRange(
            new User
            {
                Id = 1,
                Name = "Andres",
                Username = "ad_admin",
                Password = "AD123",
                Role = Role.Admin,
            },
            new User
            {
                Id = 2,
                Name = "Genos",
                Username = "genos",
                Password = "gen",
                Role = Role.Hero,
                Rank = Rank.S,
            },
            new User
            {
                Id = 3,
                Name = "Saitama",
                Username = "saitama",
                Password = "saitama1",
                Role = Role.Hero,
                Rank = Rank.C,
            }
        );
    }
}