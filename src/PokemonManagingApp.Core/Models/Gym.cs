namespace PokemonManagingApp.Core.Models;

public class Gym
{
    // Properties
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Status { get; set; } = true;
}