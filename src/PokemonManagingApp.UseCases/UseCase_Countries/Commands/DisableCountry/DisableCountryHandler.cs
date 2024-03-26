using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Commands.DisableCountry;

public class DisableCountryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DisableCountryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DisableCountryCommand request, CancellationToken cancellationToken)
    {
        Country? checkingCountry = await _unitOfWork.CountryRepository.GetEntityByConditionAsync(c => c.Id.Equals(request.Id), true);
        if (checkingCountry is null) return Result.NotFound("Country is not found");
        checkingCountry.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}