using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllPokemonsInCollection;

public class GetAllPokemonsInCollectionQuery : IRequest<Result<IEnumerable<PokemonDTO>>>
{
    public Guid OwnerId { get; set; }
}