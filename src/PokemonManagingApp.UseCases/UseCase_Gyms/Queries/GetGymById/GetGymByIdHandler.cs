using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Queries.GetGymById;

public class GetGymByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGymByIdQuery, Result<GymDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GymDTO>> Handle(GetGymByIdQuery request, CancellationToken cancellationToken)
    {
        Gym? gym = await _unitOfWork.GymRepository.GetGymByIdAsync(request.Id, cancellationToken);
        if (gym is null) return Result<GymDTO>.NotFound("Gym is not found.");
        return Result<GymDTO>.Success(gym.MapToDTO());
    }
}