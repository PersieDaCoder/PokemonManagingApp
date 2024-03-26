using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.DisableOwner;

public class DisableOwnerHandler(IUnitOfWork unitOfWork) : IRequestHandler<DisableOwnerCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DisableOwnerCommand request, CancellationToken cancellationToken)
    {
        Owner? checkingOwner = await _unitOfWork.OwnerRepository.GetEntityByConditionAsync(o => o.Id.Equals(request.Id), true);
        if (checkingOwner is null) return Result.NotFound();
        checkingOwner.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}