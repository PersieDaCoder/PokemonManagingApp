using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetOwnerById;

public class GetOwnerByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOwnerByIdQuery, Result<OwnerDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<OwnerDTO>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
    {
        Owner? owner = await _unitOfWork.OwnerRepository.GetOwnerById(request.Id, false);
        if (owner is null) return Result<OwnerDTO>.NotFound();
        return Result<OwnerDTO>.Success(OwnerMapper.MapToDTO(owner));
    }
}