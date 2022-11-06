namespace Shared.Data.Model;

public interface IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
}