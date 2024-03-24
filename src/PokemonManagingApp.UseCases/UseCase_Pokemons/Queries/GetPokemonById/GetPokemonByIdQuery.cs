using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons.Queries.GetPokemonById;

public class GetPokemonByIdQuery : IRequest<Result<PokemonDTO>>
{
  public Guid Id { get; init; }
}