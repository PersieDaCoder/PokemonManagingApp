using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCase.DTOs;

public record OwnerDTO
{
    public Guid Id { get; init; }
    public Guid CountryId { get; init; }
    public string UserName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Gym { get; init; } = null!;
    public bool Status { get; init; }
    public CountryDTO? Country{ get; init;}
    public IEnumerable<PokemonDTO> Pokemons { get; set; } = new List<PokemonDTO>();
}