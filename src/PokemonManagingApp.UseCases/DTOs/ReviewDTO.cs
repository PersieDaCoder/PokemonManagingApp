using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.DTOs;

public record ReviewDTO
{
    public Guid Id { get; init; } 
    public string Title { get; init; } = null!;
    public string Text { get; init; } = null!;
    public bool Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public PokemonDTO? Pokemon { get; set; }
    public IEnumerable<OwnerDTO> Owners {get; init;} = new List<OwnerDTO>();
}