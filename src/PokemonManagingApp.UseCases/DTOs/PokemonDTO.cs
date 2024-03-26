namespace PokemonManagingApp.UseCases.DTOs;

public class PokemonDTO : BaseEntityDTO
{
    public required string Name { get; init; }
    public required DateTime BirthDate { get; init; }
    
    public IEnumerable<OwnerDTO> Owners { get; set; } = new List<OwnerDTO>();
    public IEnumerable<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
    public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
}
