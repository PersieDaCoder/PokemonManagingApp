using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCase.DTOs;

public record ReviewerDTO
{
    public required Guid Id { get; init; } 
    public string FullName {get;init;} = null!;
    public bool Status { get; init; }
    public IEnumerable<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
}