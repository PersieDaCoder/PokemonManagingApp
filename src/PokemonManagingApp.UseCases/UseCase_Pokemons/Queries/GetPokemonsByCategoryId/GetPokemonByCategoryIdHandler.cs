using Ardalis.Result;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Queries.GetPokemonsByCategoryId;

public class GetPokemonByCategoryIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPokemonByCategoryIdQuery, Result<IEnumerable<PokemonDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<PokemonDTO>>> Handle(GetPokemonByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Pokemon> pokemons = await _unitOfWork.PokemonRepository.GetPokemonsByCategoryIdAsync(request.CategoryId);
        if(pokemons.IsNullOrEmpty()) return Result<IEnumerable<PokemonDTO>>.NotFound("Pokemon is not found in this category");
        return Result<IEnumerable<PokemonDTO>>.Success(pokemons.Select(p => p.MapToDTO()));
    }
}