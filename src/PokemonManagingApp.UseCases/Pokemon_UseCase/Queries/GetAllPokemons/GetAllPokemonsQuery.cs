using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.DTOs.Pokemon;
using MediatR;

namespace PokemonManagingApp.UseCases.Pokemon.Queries.GetAllPokemons;

public record GetAllPokemonsQuery() : IRequest<IEnumerable<PokemonDTO>>
{

}