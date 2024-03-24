namespace PokemonManagingApp.UseCases.DTOs;

public record OwnerDTO
{
    public Guid Id { get; init; }
    public string Email { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public bool Status { get; init; }
    public DateTime CreatedAt { get; init; }

    public GymDTO? Gym { get; init; }
    public CountryDTO? Country { get; init; }
    public IEnumerable<PokemonDTO> Pokemons { get; init; } = new List<PokemonDTO>();
    public IEnumerable<ReviewDTO> Reviews { get; init; } = new List<ReviewDTO>();
}