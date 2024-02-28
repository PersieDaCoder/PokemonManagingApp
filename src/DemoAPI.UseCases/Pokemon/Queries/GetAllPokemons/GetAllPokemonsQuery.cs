using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.DTOs.Pokemon;
using MediatR;

namespace DemoAPI.UseCases.Pokemon.Queries.GetAllPokemons;

public record GetAllPokemonsQuery() : IRequest<IEnumerable<PokemonDTO>>
{

}