namespace Shared.Data.Model;

public class Fight
{
    public int Id { get; set; }
    public int MonsterId { get; set; }
    public int HeroId { get; set; }
    public bool IsHeroWinner { get; set; }

    #region Relations

    public virtual Monster Monster { get; set; }
    public virtual Hero Hero { get; set; }

    #endregion
}