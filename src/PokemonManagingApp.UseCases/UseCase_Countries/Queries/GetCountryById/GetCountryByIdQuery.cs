using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetCountryById;

public record GetCountryByIdQuery : IRequest<Result<CountryDTO>>
{
    public Guid Id { get; init; }
}