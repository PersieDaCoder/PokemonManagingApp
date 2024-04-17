using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.Pokemon_UseCase;

public record UpdatePokemonCommand : IRequest<Result>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateTime BirthDate { get; init; }
    public required string Description { get; init; }
    public required string ImageUrl { get; init; }
    public required int Height { get; init; }
    public required int Weight { get; init; }
    public required Guid CategoryId { get; init; }
}
