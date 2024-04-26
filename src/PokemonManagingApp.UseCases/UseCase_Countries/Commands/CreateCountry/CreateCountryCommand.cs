using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Commands.CreateCountry;

public class CreateCountryCommand : IRequest<Result<CountryDTO>>
{
  public string Name { get; init; } = null!;
}