using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.DTOs;

public class ReviewDTO : BaseEntityDTO
{
    public string Title { get; init; } = null!;
    public string Text { get; init; } = null!;
    public PokemonDTO? Pokemon { get; set; } = null!;
    public OwnerDTO? Owner {get; init;} = null!;
}