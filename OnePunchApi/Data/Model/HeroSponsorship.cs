using System.ComponentModel.DataAnnotations;

namespace OnePunchApi.Data.Model;

public class HeroSponsorship
{
    [Required] public int SponsorId { get; set; }
    public virtual Sponsor Sponsor { get; set; } = default!;
    [Required] public int HeroId { get; set; }
    public virtual Hero Hero { get; set; } = default!;
}