using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Core.DTOs.Pokemon.Create;

public record CreatePokemonRequest
{
    public required string Name { get; init; }
}