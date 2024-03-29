using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.CreateOwner;

public class CreateOwnerCommand : IRequest<Result<OwnerDTO>>
{
  public string PhoneNumber { get; set; } = null!;
  public string Gmail { get; init; } = null!;
  public string Password { get; init; } = null!;
  public string UserName { get; init; } = null!;
  public Guid GymId { get; init; }
  public Guid CountryId { get; init; }
}