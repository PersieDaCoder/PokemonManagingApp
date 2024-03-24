namespace PokemonManagingApp.UseCases.DTOs;

public record PokemonDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateTime BirthDate { get; init; }
    public bool Status { get; init; }
    
    public IEnumerable<OwnerDTO> Owners { get; set; } = new List<OwnerDTO>();
    public IEnumerable<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
    public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
}
