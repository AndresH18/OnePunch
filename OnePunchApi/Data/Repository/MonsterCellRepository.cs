using Microsoft.EntityFrameworkCore;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Data.Repository;

public class MonsterCellRepository
{
    private readonly HeroAssociationDb _db;

    public MonsterCellRepository(HeroAssociationDb db)
    {
        _db = db;
    }

    public IEnumerable<MonsterCell> GetAll()
    {
        return _db.MonsterCells
            .Include(mc => mc.Monster)
            .ToList();
    }

    public MonsterCell? Get(int id)
    {
        return _db.MonsterCells
            .Include(mc => mc.Monster)
            .FirstOrDefault(mc => mc.Id == id);
    }

    public MonsterCell? Register(MonsterCell monsterCell, int monsterId = 0)
    {
        if (monsterId > 0)
        {
            var monster = _db.Monsters.FirstOrDefault(m => m.Id == monsterId);
            if (monster is null)
            {
                return null;
            }

            monsterCell.IsConsumed = true;
            monsterCell.Monster = monster;
        }

        _db.MonsterCells.Add(monsterCell);
        _db.SaveChanges();
        return monsterCell;
    }

    public MonsterCell? ChangeStatus(MonsterCell monsterCell, int monsterId)
    {
        if (monsterId <= 0)
            return null;

        var monster = _db.Monsters.FirstOrDefault(m => m.Id == monsterId);
        
        if (monster is null)
        {
            return null;
        }

        monsterCell.IsConsumed = true;
        monsterCell.Monster = monster;

        _db.MonsterCells.Update(monsterCell);
        _db.SaveChanges();

        return monsterCell;
    }
}