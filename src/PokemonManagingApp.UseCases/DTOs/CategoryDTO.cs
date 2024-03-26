namespace PokemonManagingApp.UseCases.DTOs;

public class CategoryDTO : BaseEntityDTO
{
    public string Name { get; init; } = null!;
    public IEnumerable<PokemonDTO> Pokemons { get; init; } = new List<PokemonDTO>();
}