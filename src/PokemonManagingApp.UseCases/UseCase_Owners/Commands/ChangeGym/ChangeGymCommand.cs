using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.ChangeGym;

public class ChangeGymCommand : IRequest<Result>
{
    public Guid OwnerId { get; init; }
    public Guid GymId { get; init; }
}