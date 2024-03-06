using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.DTOs.Review;

namespace PokemonManagingApp.Core.DTOs.Reviewer;

public record ReviewerDTO
{
    public required Guid Id { get; init; } 
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public IEnumerable<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
}