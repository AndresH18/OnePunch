using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnePunchApi.Data.Model;

public class MonsterSponsorship
{
    [Required] public int SponsorId { get; set; }
    [Required] public int MonsterId { get; set; }

    [JsonIgnore] public virtual Sponsor Sponsor { get; set; } = default!;
    [JsonIgnore] public virtual Monster Monster { get; set; } = default!;
}