using Microsoft.EntityFrameworkCore;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Data.Repository;

public class MonsterRepository : IRepository<Monster>
{
    private readonly AssociationDb _db;

    public MonsterRepository(AssociationDb db)
    {
        _db = db;
    }

    public Monster Create(Monster monster)
    {
        _db.Monsters.Add(monster);
        _db.SaveChanges();
        return monster;
    }

    public IEnumerable<Monster> GetAll()
    {
        return _db.Monsters.ToList();
    }

    public Monster? Get(int id)
    {
        return _db.Monsters.FirstOrDefault(m => m.Id == id);
    }

    public void Update(Monster monster)
    {
        throw new NotImplementedException();
    }

    public void Delete(Monster monster)
    {
        _db.Monsters.Remove(monster);
        _db.SaveChanges();
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}