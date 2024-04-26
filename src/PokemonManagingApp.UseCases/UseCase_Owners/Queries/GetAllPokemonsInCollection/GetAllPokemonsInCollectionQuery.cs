using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllPokemonsInCollection;

public record GetAllPokemonsInCollectionQuery : IRequest<Result<IEnumerable<PokemonDTO>>>
{
    public Guid OwnerId { get; init; }
}