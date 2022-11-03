using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Data.Model;

public class Sponsor : IModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = default!;

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; set; } = Status.Alive;

    #region Relations

    public virtual List<Hero> Heroes { get; set; } = new();

    public virtual List<Monster> Monsters { get; set; } = new();

    #endregion
}