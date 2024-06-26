using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public record GetAllPokemonsQuery() : IRequest<Result<IEnumerable<PokemonDTO>>>
{
}