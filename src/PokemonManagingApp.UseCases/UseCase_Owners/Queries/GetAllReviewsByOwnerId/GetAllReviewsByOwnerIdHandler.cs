using Ardalis.Result;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllReviewsByOwnerId;

public class GetAllReviewsByOwnerIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllReviewByOwnerIdQuery, Result<IEnumerable<ReviewDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<ReviewDTO>>> Handle(GetAllReviewByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ReviewDTO> result = await _unitOfWork.ReviewRepository.GetReviewsByOwnerIdAsync(request.OwnerId);
        if(result.IsNullOrEmpty()) return Result.NotFound("Reviews is not found");
        return Result.Success(result);
    }
}