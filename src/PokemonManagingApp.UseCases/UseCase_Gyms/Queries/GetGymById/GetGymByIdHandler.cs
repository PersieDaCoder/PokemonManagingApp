using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Queries.GetGymById;

public class GetGymByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGymByIdQuery, Result<GymDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GymDTO>> Handle(GetGymByIdQuery request, CancellationToken cancellationToken)
    {
        // Get the gym by id
        GymDTO? gym =
            await _unitOfWork.GymRepository.GetGymByIdAsync(request.Id, cancellationToken);
        // Check if the gym is in the database
        if (gym is null)
            return Result.NotFound("Gym is not found.");
        return Result.Success(gym);
    }
}