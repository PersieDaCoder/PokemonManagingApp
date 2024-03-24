using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Commands.UpdateCountry;

public class UpdateCountryCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}