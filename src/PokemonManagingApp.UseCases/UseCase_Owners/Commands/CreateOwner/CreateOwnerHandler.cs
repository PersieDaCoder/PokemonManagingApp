using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;
using PokemonManagingApp.Core.Enums;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.CreateOwner;

public class CreateOwnerHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOwnerCommand, Result<OwnerDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<OwnerDTO>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {
        Country? checkingCountry =
            await _unitOfWork.CountryRepository
                .GetEntityByConditionAsync(c => c.Id.Equals(request.CountryId), false);
        if (checkingCountry is null)
            return Result<OwnerDTO>.NotFound("Country is not found");
        Owner addingOwner = new()
        {
            Email = request.Gmail,
            Password = request.Password,
            CountryId = request.CountryId,
            UserName = request.UserName,
            ImageUrl = request.ImageUrl,
            GymId = request.GymId,
            Role = (int)RoleEnum.User,
        };
        _unitOfWork.OwnerRepository.Add(addingOwner);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success(addingOwner.MapToDTO(), "Owner is created successfully");
    }
}