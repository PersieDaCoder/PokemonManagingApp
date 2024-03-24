using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Commands.CreateCountry;

public class CreateCountryCommand : IRequest<Result<CountryDTO>>
{
  public string Name { get; set; } = null!;
}