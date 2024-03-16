using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetAllCountries;

public record GetAllCountriesQuery : IRequest<Result<IEnumerable<CountryDTO>>>
{
    
}