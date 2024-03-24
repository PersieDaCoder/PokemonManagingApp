namespace PokemonManagingApp.UseCases.DTOs;

public record GymDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public bool Status { get; init; }
}