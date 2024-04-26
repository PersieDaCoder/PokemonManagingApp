using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Commands.CreateGym;

public class CreateGymHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateGymCommand, Result<GymDTO>>
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result<GymDTO>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
  {
    // Check if the gym already exists
    Gym? checkingGym =
      await _unitOfWork.GymRepository
        .GetEntityByConditionAsync(g => g.Name.Trim().ToLower().Equals(request.Name.Trim().ToLower()), false);
    // If the gym already exists, return an error
    if (checkingGym is not null)
      return Result.Error("Gym already exists");
    // Create the gym
    Gym gym = new()
    {
      Name = request.Name,
    };
    // Add the gym to the database
    _unitOfWork.GymRepository.Add(gym);
    await _unitOfWork.SaveChangesAsync();
    return Result.Success(gym.MapToDTO(),"Gym is created successfully");
  }
}