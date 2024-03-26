using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.ChangeGym;

public class ChangeGymCommand : IRequest<Result>
{
    public Guid OwnerId { get; set; }
    public Guid GymId { get; set; }
}