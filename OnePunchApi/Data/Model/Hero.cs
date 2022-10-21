using System.ComponentModel.DataAnnotations;

namespace OnePunchApi.Data.Model;

public class Hero : IModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = default!;

    [Required] public Rank Rank { get; set; }

    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    
}