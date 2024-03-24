using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.AddPokemonToCollection;

public record AddPokemonToCollectionCommand : IRequest<Result<PokemonDTO>>
{
    public Guid OwnerId { get; init; }
    public Guid PokemonId { get; init; }
}