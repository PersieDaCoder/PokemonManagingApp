using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetAllCountries;

public class GetAllCountriesHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCountriesQuery, Result<IEnumerable<CountryDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<CountryDTO>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Country> countries = await _unitOfWork.CountryRepository.GetAllCountries(false);
        if (!countries.Any()) return Result<IEnumerable<CountryDTO>>.NotFound();
        return Result<IEnumerable<CountryDTO>>.Success(countries.Select(c => CountryMapper.MapToDTO(c)));
    }
}