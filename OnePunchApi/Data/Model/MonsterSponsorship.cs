using System.ComponentModel.DataAnnotations;

namespace OnePunchApi.Data.Model;

public class MonsterSponsorship
{
    [Required] public int SponsorId { get; set; }
    public virtual Sponsor Sponsor { get; set; } = default!;

    [Required] public int MonsterId { get; set; }
    public virtual Monster Monster { get; set; } = default!;
}