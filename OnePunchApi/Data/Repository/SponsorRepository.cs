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
}