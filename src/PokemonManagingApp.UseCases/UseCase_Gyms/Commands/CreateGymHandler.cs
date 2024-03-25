using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Commands;

public class CreateGymHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateGymCommand, Result<GymDTO>>
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result<GymDTO>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
  {
    Gym? checkingGym = await _unitOfWork.GymRepository.GetEntityByConditionAsync(g => g.Name.Trim().ToLower().Equals(request.Name.Trim().ToLower()), false);
    if (checkingGym is not null) return Result.Error("Gym already exists");
    Gym gym = new Gym
    {
      Name = request.Name,
    };
    _unitOfWork.GymRepository.Add(gym);
    await _unitOfWork.SaveChangesAsync();
    return Result.Success(gym.MapToDTO());
  }
}