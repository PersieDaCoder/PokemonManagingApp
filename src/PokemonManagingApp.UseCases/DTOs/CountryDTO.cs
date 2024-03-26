namespace PokemonManagingApp.UseCases.DTOs;

public record CountryDTO
{
  // Properties
  public Guid Id { get; init; } = Guid.NewGuid();
  public string Name { get; init; } = null!;
  public bool IsDeleted { get; init; }
  public DateTime CreatedAt { get; init; }
  // Navigation properties
  public IEnumerable<OwnerDTO> Owners { get; init; } = new List<OwnerDTO>();
}