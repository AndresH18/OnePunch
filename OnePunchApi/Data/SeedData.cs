using OnePunchApi.Data.Model;

namespace OnePunchApi.Data;

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

        db.SaveChanges();
    }
}