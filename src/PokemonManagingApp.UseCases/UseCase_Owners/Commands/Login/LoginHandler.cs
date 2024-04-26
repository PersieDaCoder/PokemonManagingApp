
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.Login;

public class LoginHandler(IOwnerRepository ownerRepository) : IRequestHandler<LoginCommand, Result<OwnerDTO>>
{
  private readonly IOwnerRepository _ownerRepository = ownerRepository;

  public async Task<Result<OwnerDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    Owner? owner =
      await _ownerRepository
        .GetEntityByConditionAsync(owner => owner.Email == request.Email, false);
    if (owner is null)
      return Result.NotFound("Email is not found");
    if (owner.Password != request.Password) return Result.Error("Password is incorrect");

    return Result.Success(owner.MapToDTO());
  }
}