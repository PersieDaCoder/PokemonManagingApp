using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.ChangeCountry;

public class ChangeCountryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeCountryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangeCountryCommand request, CancellationToken cancellationToken)
    {
        // Check owner
        Owner? checkingOwner = await _unitOfWork.OwnerRepository
            .GetEntityByConditionAsync(owner => owner.Id.Equals(request.OwnerId), true);
        if (checkingOwner is null) return Result.NotFound("Owner is not found");
        // Check country
        Country? checkingCountry = await _unitOfWork.CountryRepository
            .GetEntityByConditionAsync(country => country.Id.Equals(request.CountryId), false);
        if (checkingCountry is null) return Result.NotFound("Country is not found");
        // Change country
        checkingOwner.CountryId = request.CountryId;
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessWithMessage("Country is changed successfully");
    }
}