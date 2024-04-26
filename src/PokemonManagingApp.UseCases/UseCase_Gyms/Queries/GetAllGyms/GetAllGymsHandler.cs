using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;
using Microsoft.IdentityModel.Tokens;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Queries.GetAllGyms;

public class GetAllGymsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllGymsQuery, Result<IEnumerable<GymDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<GymDTO>>> Handle(GetAllGymsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<GymDTO> gyms =
            await _unitOfWork.GymRepository
                .GetGymsAsync(cancellationToken);
        if (gyms.IsNullOrEmpty())
            return Result.NotFound("Gyms are not found.");
        return Result.Success(gyms);
    }
}