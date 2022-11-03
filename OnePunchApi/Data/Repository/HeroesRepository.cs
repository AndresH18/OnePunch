using Shared.Data.Model;

namespace OnePunchApi.Data.Repository;

public class HeroesRepository
{
    private readonly HeroAssociationDb _db;

    public HeroesRepository(HeroAssociationDb db)
    {
        _db = db;
    }

    public Hero Create(Hero hero)
    {
        _db.Heroes.Add(hero);
        _db.SaveChanges();
        return hero;
    }

    public IEnumerable<Hero> GetAll()
    {
        return _db.Heroes.ToList();
    }

    public IEnumerable<Hero> GetTop(int amount)
    {
        if (amount > 0)
        {
            return _db.Heroes.OrderBy(h => h.Rank).ThenBy(h => h.Id).Take(amount);
        }

        return Array.Empty<Hero>();
    }

    public Hero? Get(int id)
    {
        return _db.Heroes.FirstOrDefault(h => h.Id == id);
    }


    public void ChangeRank(Hero hero, Rank rank)
    {
        hero.Rank = rank;
        SaveChanges();
    }

    public void ChangeStatus(Hero hero, Status status)
    {
        hero.Status = status;
        SaveChanges();
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}