using Microsoft.EntityFrameworkCore;
using Shared.Data.Model;

namespace OnePunchApi.Data.Repository;

public class FightsRepository
{
    private readonly HeroAssociationDb _db;

    public FightsRepository(HeroAssociationDb db)
    {
        _db = db;
    }

    public IEnumerable<Fight> GetAll()
    {
        return _db.Fights
            .Include(f => f.Monster)
            .Include(f => f.Hero)
            .ToList();
    }

    public IEnumerable<Fight> Get(int id)
    {
        return _db.Fights
            .Include(f => f.Monster)
            .Include(f => f.Hero)
            .Where(f => f.Id == id).ToList();
    }
}