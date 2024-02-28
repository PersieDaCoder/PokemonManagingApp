using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.DTOs.Owner;

namespace DemoAPI.Core.DTOs.Country;

  public record CountryDTO
  {
      public required Guid Id { get; init; }
      public required string Name { get; init; }
      public IEnumerable<OwnerDTO> Owners{ get; init;} = new List<OwnerDTO>();
  }