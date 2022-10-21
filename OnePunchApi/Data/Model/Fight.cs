using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnePunchApi.Data.Model;

public class Fight
{
    public int Id { get; set; }
    public int MonsterId { get; set; }
    public int HeroId { get; set; }

    public bool IsHeroWinner { get; set; }

    #region Relations

    [JsonIgnore] public virtual Monster Monster { get; set; }
    [JsonIgnore] public virtual Hero Hero { get; set; }

    #endregion
}