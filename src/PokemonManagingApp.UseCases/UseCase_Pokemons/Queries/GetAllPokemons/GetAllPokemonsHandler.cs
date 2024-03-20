using PokemonManagingApp.Core.Interfaces.Data;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases;
using PokemonManagingApp.Pokemon_UseCase;
using PokemonManagingApp.Core.Models;
using Ardalis.Result;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public class GetAllPokemonsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPokemonsQuery, Result<IEnumerable<PokemonDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<PokemonDTO>>> Handle(GetAllPokemonsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Pokemon> pokemons = await _unitOfWork.PokemonRepository.GetAllPokemonsAsync(cancellationToken);
        if (!pokemons.Any()) return Result<IEnumerable<PokemonDTO>>.NotFound();
        return Result<IEnumerable<PokemonDTO>>.Success(pokemons.Select(p => PokemonMapper.MapToDTO(p)));
    }
}