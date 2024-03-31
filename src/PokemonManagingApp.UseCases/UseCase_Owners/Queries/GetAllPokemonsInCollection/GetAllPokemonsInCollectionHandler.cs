using Ardalis.Result;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllPokemonsInCollection;

public class GetAllPokemonsInCollectionHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPokemonsInCollectionQuery, Result<IEnumerable<PokemonDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<PokemonDTO>>> Handle(GetAllPokemonsInCollectionQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<PokemonDTO> pokemons = await _unitOfWork.PokemonRepository.GetPokemonsByOwnerIdAsync(request.OwnerId, cancellationToken);
        if (pokemons.IsNullOrEmpty()) return Result<IEnumerable<PokemonDTO>>.NotFound("Pokemons are not found in collection");
        return Result<IEnumerable<PokemonDTO>>.Success(pokemons);
    }
}