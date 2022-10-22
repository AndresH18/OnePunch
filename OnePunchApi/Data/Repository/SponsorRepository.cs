using OnePunchApi.Data.Model;

namespace OnePunchApi.Data.Repository;

public class SponsorRepository
{
    private readonly AssociationDb _db;

    public SponsorRepository(AssociationDb db)
    {
        _db = db;
    }

    public Sponsor Create(Sponsor sponsor)
    {
        _db.Sponsors.Add(sponsor);
        _db.SaveChanges();
        return sponsor;
    }

    public IEnumerable<Sponsor> GetAll()
    {
        return _db.Sponsors.ToList();
    }

    public Sponsor? Get(int id)
    {
        return _db.Sponsors.FirstOrDefault(s => s.Id == id);
    }

    public void ChangeStatus(Sponsor sponsor, Status status)
    {
        sponsor.Status = status;
        _db.SaveChanges();
    }

    public Hero? AddHero(Sponsor sponsor, int heroId)
    {
        var hero = _db.Heroes.FirstOrDefault(h => h.Id == heroId);
        if (hero is null)
            return null;

        sponsor.Heroes.Add(hero);
        _db.SaveChanges();

        return hero;
    }

    public void RemoveHero(Sponsor sponsor, int monsterId)
    {
        var hero = _db.Heroes.FirstOrDefault(h => h.Id == monsterId);
        if (hero is null)
            return;

        sponsor.Heroes.Remove(hero);
        _db.SaveChanges();
    }


    public Monster? AddMonster(Sponsor sponsor, int monsterId)
    {
        var monster = _db.Monsters.FirstOrDefault(h => h.Id == monsterId);
        if (monster is null)
            return null;

        sponsor.Monsters.Add(monster);
        _db.SaveChanges();

        return monster;
    }

    public void RemoveMonster(Sponsor sponsor, int monsterId)
    {
        var monster = _db.Monsters.FirstOrDefault(h => h.Id == monsterId);
        if (monster is null)
            return;

        sponsor.Monsters.Remove(monster);
        _db.SaveChanges();
    }
}