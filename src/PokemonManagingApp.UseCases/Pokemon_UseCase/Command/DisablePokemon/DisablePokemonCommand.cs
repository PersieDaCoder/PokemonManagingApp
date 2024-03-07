using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases;

public record DisablePokemonCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}
