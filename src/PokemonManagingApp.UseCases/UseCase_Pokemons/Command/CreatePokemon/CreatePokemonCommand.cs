using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public record CreatePokemonCommand : IRequest<Result<PokemonDTO>>
{
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; }
    public required Guid CategoryId { get; set; }
}
