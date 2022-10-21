using System.Text.Json.Serialization;

namespace OnePunchApi.Data.Model;

public class MonsterCell
{
    public int Id { get; set; }
    public bool IsConsumed { get; set; }

    public int? MonsterId { get; set; }

    #region Relations

    [JsonIgnore] public virtual Monster? Monster { get; set; }

    #endregion
}