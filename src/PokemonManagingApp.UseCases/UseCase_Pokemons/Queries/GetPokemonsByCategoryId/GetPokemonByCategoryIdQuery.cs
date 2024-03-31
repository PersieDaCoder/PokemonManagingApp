using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Queries.GetPokemonsByCategoryId;

public record GetPokemonByCategoryIdQuery : IRequest<Result<IEnumerable<PokemonDTO>>>
{
    public Guid CategoryId { get; init; }
}