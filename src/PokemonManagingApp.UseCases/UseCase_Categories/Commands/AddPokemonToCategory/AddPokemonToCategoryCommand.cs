using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.AddPokemonToCategory;

public record AddPokemonToCategoryCommand : IRequest<Result<PokemonDTO>>
{
    public Guid CategoryId { get; init; }
    public Guid PokemonId { get; init; }
}