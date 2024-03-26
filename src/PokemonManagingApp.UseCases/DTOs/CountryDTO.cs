namespace PokemonManagingApp.UseCases.DTOs;

public class CountryDTO : BaseEntityDTO
{
  // Properties
  public string Name { get; init; } = null!;
  // Navigation properties
  public IEnumerable<OwnerDTO> Owners { get; init; } = new List<OwnerDTO>();
}