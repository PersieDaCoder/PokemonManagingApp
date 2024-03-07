using PokemonManagingApp.Core.DTOs.Pokemon;

namespace PokemonManagingApp.Core.DTOs.Category;

public record CategoryDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public bool Status { get; init; }
    public IEnumerable<PokemonDTO> Pokemons { get; init;} = new List<PokemonDTO>();
}