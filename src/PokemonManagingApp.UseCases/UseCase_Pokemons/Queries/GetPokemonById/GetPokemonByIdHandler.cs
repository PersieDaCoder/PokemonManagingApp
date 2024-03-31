using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons.Queries.GetPokemonById;

public class GetPokemonByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPokemonByIdQuery, Result<PokemonDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PokemonDTO>> Handle(GetPokemonByIdQuery request, CancellationToken cancellationToken)
    {
        PokemonDTO? pokemon = await _unitOfWork.PokemonRepository.GetPokemonByIdAsync(request.Id,cancellationToken);
        if (pokemon is null) return Result<PokemonDTO>.NotFound();
        return Result<PokemonDTO>.Success(pokemon);
    }
}