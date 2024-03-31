using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetOwnerById;

public class GetOwnerByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOwnerByIdQuery, Result<OwnerDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<OwnerDTO>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
    {
        OwnerDTO? owner = await _unitOfWork.OwnerRepository.GetOwnerById(request.Id, cancellationToken);
        if (owner is null) return Result<OwnerDTO>.NotFound();
        return Result<OwnerDTO>.Success(owner);
    }
}