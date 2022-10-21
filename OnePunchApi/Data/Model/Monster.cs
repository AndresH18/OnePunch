using System.ComponentModel.DataAnnotations;

namespace OnePunchApi.Data.Model;

public class Monster : IModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = default!;
    
    [Required] public DisasterLevel DisasterLevel { get; set; }
    
    // TODO: Fights (id, monster, hero) 
    
}