using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public record CreatePokemonCommand : IRequest<Result<PokemonDTO>>
{
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; }
}
