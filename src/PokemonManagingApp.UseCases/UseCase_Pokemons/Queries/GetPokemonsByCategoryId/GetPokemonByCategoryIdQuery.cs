using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Queries.GetPokemonsByCategoryId;

public class GetPokemonByCategoryIdQuery : IRequest<Result<IEnumerable<PokemonDTO>>>
{
    public Guid CategoryId { get; set; }
}