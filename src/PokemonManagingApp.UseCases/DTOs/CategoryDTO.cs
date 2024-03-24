namespace PokemonManagingApp.UseCase.DTOs;

public record CategoryDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public bool Status { get; init; }
    public IEnumerable<PokemonDTO> Pokemons { get; init;} = new List<PokemonDTO>();
}