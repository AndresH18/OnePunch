using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnePunchApi.Data.Model;

public class Monster : IModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = default!;

    [Required] public DisasterLevel DisasterLevel { get; set; }


    #region Relations

    [JsonIgnore] public virtual List<Sponsor> Sponsors { get; set; } = new();
    [JsonIgnore] public virtual List<Fight> Fights { get; set; } = new();
    [JsonIgnore] public virtual MonsterCell MonsterCell { get; set; }

    #endregion
}