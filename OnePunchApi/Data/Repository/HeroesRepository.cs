using Microsoft.EntityFrameworkCore;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Data.Repository;

public class HeroesRepository : IRepository<Hero>
{
    private readonly AssociationDb _db;

    public HeroesRepository(AssociationDb db)
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

    public Hero? Get(int id)
    {
        return _db.Heroes.FirstOrDefault(h => h.Id == id);
    }

    public void Update(Hero t)
    {
        throw new NotImplementedException();
    }

    public void Delete(Hero hero)
    {
        _db.Heroes.Remove(hero);
        _db.SaveChanges();
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}