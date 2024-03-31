using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetAllCountries;

public class GetAllCountriesHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCountriesQuery, Result<IEnumerable<CountryDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<CountryDTO>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<CountryDTO> countries = await _unitOfWork.CountryRepository.GetAllCountriesAsync(cancellationToken);
        if (!countries.Any()) return Result<IEnumerable<CountryDTO>>.NotFound();
        return Result<IEnumerable<CountryDTO>>.Success(countries);
    }
}