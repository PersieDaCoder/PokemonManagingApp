using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.RemovePokemonOutOfCollection;

public class RemovePokemonOutOfCollectionHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemovePokemonOutOfCollectionCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemovePokemonOutOfCollectionCommand request, CancellationToken cancellationToken)
    {
        //check owner existance
        Owner? checkingOwner =
            await _unitOfWork.OwnerRepository
                .GetEntityByConditionAsync(owner => owner.Id.Equals(request.OwnerId), false);
        if (checkingOwner is null)
            return Result.NotFound("Owner not found");
        //check pokemon existance
        Pokemon? checkingPokemon =
            await _unitOfWork.PokemonRepository
                .GetEntityByConditionAsync(pokemon => pokemon.Id.Equals(request.PokemonId), false);
        if (checkingPokemon is null)
            return Result.NotFound("Pokemon not found");
        //check pokemon in owner's collection
        PokemonOwner? checkingPokemonOwner =
            await _unitOfWork.PokemonOwnerRepository
                .GetEntityByConditionAsync(pokemonOwner => pokemonOwner.OwnerId.Equals(request.OwnerId) && pokemonOwner.PokemonId.Equals(request.PokemonId), false);
        if (checkingPokemonOwner is null)
            return Result.NotFound("Pokemon not found in Owner's Collection");
        //remove pokemon from owner's collection
        _unitOfWork.PokemonOwnerRepository.Remove(checkingPokemonOwner);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}