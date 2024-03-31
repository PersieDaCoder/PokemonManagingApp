using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.AddPokemonToCollection;

public class AddPokemonToCollectionHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddPokemonToCollectionCommand, Result<PokemonDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PokemonDTO>> Handle(AddPokemonToCollectionCommand request, CancellationToken cancellationToken)
    {
        Owner? checkingOwner = await _unitOfWork.OwnerRepository.GetEntityByConditionAsync(o => o.Id.Equals(request.OwnerId), false);
        if (checkingOwner is null) return Result.NotFound("Owner is not found");
        Pokemon? checkingPokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id == request.PokemonId, false);
        if (checkingPokemon is null) return Result.NotFound("Pokemon is not found");
        PokemonOwner? checkingPokemonOwner = await _unitOfWork.PokemonOwnerRepository.GetEntityByConditionAsync(po => po.OwnerId == request.OwnerId && po.PokemonId == request.PokemonId, false);
        if (checkingPokemonOwner is not null) return Result.Error("Pokemon is already owned by this owner");
        PokemonOwner newPokemonOwner = new PokemonOwner
        {
            OwnerId = request.OwnerId,
            PokemonId = request.PokemonId
        };
        _unitOfWork.PokemonOwnerRepository.Add(newPokemonOwner);
        await _unitOfWork.SaveChangesAsync();
        return Result<PokemonDTO>.Success(checkingPokemon.MapToDTO());
    }
}