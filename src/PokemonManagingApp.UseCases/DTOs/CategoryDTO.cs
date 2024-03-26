namespace PokemonManagingApp.UseCases.DTOs;

public record CategoryDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public bool IsDeleted { get; init; }
    public DateTime CreatedAt { get; init; }
    public IEnumerable<PokemonDTO> Pokemons { get; init; } = new List<PokemonDTO>();
}