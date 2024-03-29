using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Queries.GetAllGyms;

public class GetAllGymsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllGymsQuery, Result<IEnumerable<GymDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<GymDTO>>> Handle(GetAllGymsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Gym> gyms = await _unitOfWork.GymRepository.GetGymsAsync(cancellationToken);
        if (!gyms.Any()) return Result<IEnumerable<GymDTO>>.NotFound("Gyms are not found.");
        return Result<IEnumerable<GymDTO>>.Success(gyms.Select(g => g.MapToDTO()));
    }
}