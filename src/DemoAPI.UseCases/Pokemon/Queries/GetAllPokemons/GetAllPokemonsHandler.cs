using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.DTOs.Pokemon;
using DemoAPI.Core.Interfaces.Data;
using MediatR;

namespace DemoAPI.UseCases.Pokemon.Queries.GetAllPokemons;

public class GetAllPokemonsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPokemonsQuery, IEnumerable<PokemonDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<PokemonDTO>> Handle(GetAllPokemonsQuery request, CancellationToken cancellationToken)
     => await _unitOfWork.PokemonRepository.GetAllDTOs();
}