using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;
using Microsoft.IdentityModel.Tokens;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllOwners;

public class GetAllOwnersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOwnersQuery, Result<IEnumerable<OwnerDTO>>>
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result<IEnumerable<OwnerDTO>>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
  {
    // Get all owners
    IEnumerable<OwnerDTO> owners =
      await _unitOfWork.OwnerRepository.GetAllOwners(cancellationToken);
    if (owners.IsNullOrEmpty())
      return Result.NotFound("Owners is not found");
    return Result.Success(owners);
  }
}