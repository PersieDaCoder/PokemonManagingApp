using PokemonManagingApp.Core.Interfaces.Data;
using MediatR;
using Ardalis.Result;
using PokemonManagingApp.Core.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public class GetAllPokemonsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPokemonsQuery, Result<IEnumerable<PokemonDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<PokemonDTO>>> Handle(GetAllPokemonsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<PokemonDTO> pokemons = await _unitOfWork.PokemonRepository.GetAllPokemonsAsync(cancellationToken);
        if (pokemons.IsNullOrEmpty()) return Result.NotFound("Pokemons is not found");
        return Result.Success(pokemons);
    }
}