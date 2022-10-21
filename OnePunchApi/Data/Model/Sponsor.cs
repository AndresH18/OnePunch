using System.ComponentModel.DataAnnotations;

namespace OnePunchApi.Data.Model;

public class Sponsor : IModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = default!;

    #region Relations

    public virtual List<Hero> Heroes { get; set; } = new();
    public virtual List<Monster> Monsters { get; set; } = new();

    #endregion
}