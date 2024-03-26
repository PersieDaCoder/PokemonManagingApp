using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.ChangeGym;

public class ChangeGymHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeGymCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangeGymCommand request, CancellationToken cancellationToken)
    {
        // Get the owner by the owner id
        Owner? owner = await _unitOfWork.OwnerRepository.GetEntityByConditionAsync(owner => owner.Id.Equals(request.OwnerId), true);
        if (owner is null) return Result.NotFound("Owner is not found");
        // Get the gym by the gym id
        Gym? gym = await _unitOfWork.GymRepository.GetEntityByConditionAsync(gym => gym.Id.Equals(request.GymId), false);
        if (gym is null) return Result.NotFound("Gym is not found");
        // Change the gym of the owner
        owner.GymId = request.GymId;
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}