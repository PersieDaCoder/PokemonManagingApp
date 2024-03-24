using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetAllCountries;

public record GetAllCountriesQuery : IRequest<Result<IEnumerable<CountryDTO>>>
{

}