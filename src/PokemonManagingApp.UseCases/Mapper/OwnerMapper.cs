using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.Mapper;

public static class OwnerMapper
{
    public static OwnerDTO MapToDTO(this Owner owner)
      => new OwnerDTO
      {
          Id = owner.Id,
          Name = owner.Name,
          CountryId = owner.CountryId,
          Gym = owner.Gym,
          Status = owner.Status,
          Country = owner.Country is null ? null! : new CountryDTO
          {
              Id = owner.Country.Id,
              Name = owner.Country.Name,
              Status = owner.Country.Status
          },
          Pokemons = owner.PokemonOwners is null ? new List<PokemonDTO>() :
              owner.PokemonOwners
              .Select(po => po.Pokemon)
              .Select(p => p == null ? null! : PokemonMapper.MapToDTO(p))
              .ToList(),
      };
}