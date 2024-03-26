namespace PokemonManagingApp.Core.Models;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool Status { get; set; } = true;
}