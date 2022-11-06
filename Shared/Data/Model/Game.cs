using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Model;

public class Game
{
    [Key, Required] public int Id { get; set; }
    [Required] public string Name { get; set; }
    public bool IsFavorite { get; set; } = false;
}