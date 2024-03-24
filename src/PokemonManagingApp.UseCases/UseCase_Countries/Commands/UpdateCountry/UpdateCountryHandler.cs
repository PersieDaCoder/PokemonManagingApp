using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Commands.UpdateCountry;

public class UpdateCountryHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCountryCommand, Result>
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
  {
    Country? country = await _unitOfWork.CountryRepository.GetEntityByConditionAsync(c => c.Id.Equals(request.Id), true);
    if (country is null) return Result.Error("Country is not found");
    {
      country.Name = request.Name;
    }
    await _unitOfWork.SaveChangesAsync();
    return Result.Success();
  }
}