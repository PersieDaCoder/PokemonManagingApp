using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public record GetAllPokemonsQuery() : IRequest<Result<IEnumerable<PokemonDTO>>>
{
}