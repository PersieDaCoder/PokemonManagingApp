using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCase.DTOs;

public record ReviewerDTO
{
    public required Guid Id { get; init; } 
    public string FullName {get;init;} = null!;
    public bool Status { get; init; }
    public IEnumerable<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
}