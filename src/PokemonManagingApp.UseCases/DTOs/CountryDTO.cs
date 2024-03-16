using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCase.DTOs;

  public record CountryDTO
  {
      public required Guid Id { get; init; }
      public required string Name { get; init; }
      public bool Status { get; init; }
      public IEnumerable<OwnerDTO> Owners{ get; init;} = new List<OwnerDTO>();
  }