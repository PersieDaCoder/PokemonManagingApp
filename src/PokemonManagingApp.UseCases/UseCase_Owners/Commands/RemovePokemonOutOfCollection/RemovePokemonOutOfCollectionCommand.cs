using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.RemovePokemonOutOfCollection;

public class RemovePokemonOutOfCollectionCommand : IRequest<Result>
{
    public Guid OwnerId { get; init; }
    public Guid PokemonId { get; init; }
}