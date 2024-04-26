using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.CreateOwner;

public class CreateOwnerCommand : IRequest<Result<OwnerDTO>>
{
  public string PhoneNumber { get; init; } = null!;
  public string Gmail { get; init; } = null!;
  public string Password { get; init; } = null!;
  public string UserName { get; init; } = null!;
  public string ImageUrl { get; init; } = null!;
  public Guid GymId { get; init; }
  public Guid CountryId { get; init; }
}