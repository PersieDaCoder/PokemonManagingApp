using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public record DisablePokemonCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}
