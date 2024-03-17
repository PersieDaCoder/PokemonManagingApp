using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCase.DTOs;

public record OwnerDTO
{
    public required Guid Id { get; init; }
    public required Guid CountryId { get; init; }
    public required string Name { get; init; }
    public required string Gym { get; init; }
    public bool Status { get; init; }
    public CountryDTO? Country{ get; init;}
    public IEnumerable<PokemonDTO> Pokemons { get; set; } = new List<PokemonDTO>();
}