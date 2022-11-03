using Shared.Data.Model;

namespace OnePunchApi.Data.Repository;

public class MonsterRepository
{
    private readonly HeroAssociationDb _db;

    public MonsterRepository(HeroAssociationDb db)
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

    public void ChangeDisaster(Monster monster, DisasterLevel disasterLevel)
    {
        monster.DisasterLevel = disasterLevel;
        SaveChanges();
    }

    public void ChangeStatus(Monster monster, Status status)
    {
        monster.Status = Status.Dead;
        SaveChanges();
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}