using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public record CreatePokemonCommand : IRequest<Result<PokemonDTO>>
{
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; }
    public required Guid CategoryId { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required int Height { get; set; }
    public required int Weight { get; set; }
}
