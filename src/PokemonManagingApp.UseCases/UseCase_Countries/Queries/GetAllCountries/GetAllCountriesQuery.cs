using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetAllCountries;

public record GetAllCountriesQuery : IRequest<Result<IEnumerable<CountryDTO>>>
{
}