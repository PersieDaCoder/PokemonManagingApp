namespace PokemonManagingApp.Core.DTOs;

public class OwnerDTO : BaseEntityDTO
{
    public string Email { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;

    public GymDTO? Gym { get; init; }
    public CountryDTO? Country { get; init; }
    public IEnumerable<PokemonDTO> Pokemons { get; init; } = new List<PokemonDTO>();
    public IEnumerable<ReviewDTO> Reviews { get; init; } = new List<ReviewDTO>();
}