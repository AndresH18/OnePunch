using System.ComponentModel.DataAnnotations;

namespace OnePunchApi.Data.Model;

public class Sponsor : IModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = default!;
}