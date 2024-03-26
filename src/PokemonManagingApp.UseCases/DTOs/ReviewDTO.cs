using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.DTOs;

public record ReviewDTO
{
    public Guid Id { get; init; } 
    public string Title { get; init; } = null!;
    public string Text { get; init; } = null!;
    public bool IsDeleted { get; init; }
    public DateTime CreatedAt { get; init; }
    public PokemonDTO? Pokemon { get; set; } = null!;
    public OwnerDTO? Owner {get; init;} = null!;
}