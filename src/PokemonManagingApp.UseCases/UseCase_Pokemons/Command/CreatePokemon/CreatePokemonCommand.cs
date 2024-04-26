using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public record CreatePokemonCommand : IRequest<Result<PokemonDTO>>
{
    public required string Name { get; init; }
    public required DateTime BirthDate { get; init; }
    public required Guid CategoryId { get; init; }
    public required string Description { get; init; }
    public required string ImageUrl { get; init; }
    public required int Height { get; init; }
    public required int Weight { get; init; }
}
