using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonManagingApp.Core.DTOs.Pokemon.Create;

public record CreatePokemonRequest
{
    public required string Name { get; init; }
}