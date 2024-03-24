using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCase.DTOs;

  public record CountryDTO
  {
      public Guid Id { get; init; } = Guid.NewGuid();
      public string Name { get; init; } = null!;
      public bool Status { get; init; }
      public IEnumerable<OwnerDTO> Owners{ get; init;} = new List<OwnerDTO>();
  }