using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public record GetAllPokemonsQuery() : IRequest<IEnumerable<PokemonDTO>>
{
}