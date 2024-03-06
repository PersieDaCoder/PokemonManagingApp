using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.DTOs.Category;
using DemoAPI.Core.DTOs.Owner;
using DemoAPI.Core.DTOs.Review;

namespace DemoAPI.Core.DTOs.Pokemon;

public record PokemonDTO
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateTime BirthDate { get; init; }
    public IEnumerable<OwnerDTO> Owners { get; set; } = new List<OwnerDTO>();
    public IEnumerable<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
    public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
}
