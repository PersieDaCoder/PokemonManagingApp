using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.UpdateOwner;

public class UpdateOwnerHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateOwnerCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        Owner? checkingOwner = await _unitOfWork.OwnerRepository.GetEntityByConditionAsync(o => o.Id.Equals(request.Id), true);
        Country? checkingCountry = await _unitOfWork.CountryRepository.GetEntityByConditionAsync(c => c.Id.Equals(request.CountryId), false);
        if (checkingOwner is null) return Result.NotFound("Owner is not found");
        if (checkingCountry is null) return Result.NotFound("Country is not found");
        {
            checkingOwner.Name = request.Name;
            checkingOwner.Gym = request.Gym;
            checkingOwner.CountryId = request.CountryId;
        }
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}