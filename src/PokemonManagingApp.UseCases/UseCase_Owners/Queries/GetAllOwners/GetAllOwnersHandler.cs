using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllOwners;

public class GetAllOwnersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOwnersQuery, Result<IEnumerable<OwnerDTO>>>
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result<IEnumerable<OwnerDTO>>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
  {
    IEnumerable<Owner> owners = await _unitOfWork.OwnerRepository.GetAllOwners(cancellationToken);
    if (!owners.Any()) return Result<IEnumerable<OwnerDTO>>.NotFound();
    return Result<IEnumerable<OwnerDTO>>.Success(owners.Select(o => o.MapToDTO()));
  }
}