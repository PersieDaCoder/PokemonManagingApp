using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using PokemonManagingApp.Core.DTOs.Pokemon;
using MediatR;

namespace PokemonManagingApp.UseCases;

public record CreatePokemonCommand : IRequest<Result<PokemonDTO>>
{
    [MaxLength(50,ErrorMessage = "Name must be less than 50 characters")]
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; }
}
