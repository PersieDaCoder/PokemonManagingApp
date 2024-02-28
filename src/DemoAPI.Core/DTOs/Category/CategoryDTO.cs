using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.DTOs.Pokemon;

namespace DemoAPI.Core.DTOs.Category;

public record CategoryDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public IEnumerable<PokemonDTO> Pokemons { get; init;} = new List<PokemonDTO>();
}