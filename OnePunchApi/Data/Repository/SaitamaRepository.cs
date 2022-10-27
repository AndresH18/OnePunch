using OnePunchApi.Data.Model;

namespace OnePunchApi.Data.Repository;

public class SaitamaRepository
{
    private readonly HeroAssociationDb _db;

    public SaitamaRepository(HeroAssociationDb db)
    {
        _db = db;
    }

    public IEnumerable<Game> GetFavoriteGames()
    {
        return _db.Games.Where(g => g.IsFavorite).ToList();
    }

    public Game? GetFavoriteGame(int id)
    {
        return _db.Games.Where(g => g.IsFavorite).FirstOrDefault(g => g.Id == id);
    }
}