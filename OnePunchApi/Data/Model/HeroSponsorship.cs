using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnePunchApi.Data.Model;

public class HeroSponsorship
{
    [Required] public int SponsorId { get; set; }
    [Required] public int HeroId { get; set; }
    
    [JsonIgnore] public virtual Sponsor Sponsor { get; set; } = default!;
    [JsonIgnore] public virtual Hero Hero { get; set; } = default!;
}