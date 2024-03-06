using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.DTOs.Country;
using PokemonManagingApp.Core.DTOs.Pokemon;

namespace PokemonManagingApp.Core.DTOs.Owner;

public record OwnerDTO
{
    public required Guid Id { get; init; }
    public required Guid CountryId { get; init; }
    public required string Name { get; init; }
    public required string Gym { get; init; }
    public CountryDTO? Country{ get; init;}
    public IEnumerable<PokemonDTO> Pokemon { get; set; } = new List<PokemonDTO>();
}