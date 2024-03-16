using PokemonManagingApp.Core.Interfaces.Data;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Pokemon_UseCase;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public class GetAllPokemonsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPokemonsQuery, IEnumerable<PokemonDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<PokemonDTO>> Handle(GetAllPokemonsQuery request, CancellationToken cancellationToken)
     => await _unitOfWork.PokemonRepository.DBSet()
     .AsNoTracking()
     .Where(p => p.Status)
    .Select(p => PokemonMapper.MapToPokemonDTO(p))
    .ToListAsync(cancellationToken: cancellationToken);
}