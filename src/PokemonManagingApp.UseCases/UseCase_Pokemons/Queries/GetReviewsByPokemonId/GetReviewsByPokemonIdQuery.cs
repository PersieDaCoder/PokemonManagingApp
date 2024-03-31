using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons.Queries.GetReviewsByPokemonId;

public record GetReviewsByPokemonIdQuery : IRequest<Result<IEnumerable<ReviewDTO>>>
{
    public Guid PokemonId { get; init; }
}