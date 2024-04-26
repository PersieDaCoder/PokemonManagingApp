using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetAllCountries;

public class GetAllCountriesHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCountriesQuery, Result<IEnumerable<CountryDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<CountryDTO>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        // Get all countries
        IEnumerable<CountryDTO> countries =
            await _unitOfWork.CountryRepository.GetAllCountriesAsync(cancellationToken);
        if (countries.IsNullOrEmpty())
            return Result.NotFound("Countries is not found");
        return Result.Success(countries);
    }
}