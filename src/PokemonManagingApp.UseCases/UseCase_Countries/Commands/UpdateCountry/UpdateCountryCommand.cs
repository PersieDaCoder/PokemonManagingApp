using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Commands.UpdateCountry;

public class UpdateCountryCommand : IRequest<Result<CountryDTO>>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}