using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.DTOs.Pokemon;

namespace DemoAPI.Core.DTOs.Review;

public record ReviewDTO
{
    public required Guid Id { get; init; } 
    public required string Title { get; init; }
    public required string Text { get; init; }
    public required Guid ReviewerId { get; init; }
    public required Guid PokemonId { get; init; }
    public PokemonDTO? Pokemon { get; set; }
    public ReviewDTO? Reviewer { get; set; }
}