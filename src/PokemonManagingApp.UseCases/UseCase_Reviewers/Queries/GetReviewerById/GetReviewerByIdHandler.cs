using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Queries.GetReviewerById;

public class GetReviewerByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetReviewerByIdQuery, Result<ReviewerDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ReviewerDTO>> Handle(GetReviewerByIdQuery request, CancellationToken cancellationToken)
    {
        Reviewer? reviewer = await _unitOfWork.ReviewerRepository.GetReviewerByIdAsync(request.Id, false);
        if (reviewer == null) return Result<ReviewerDTO>.NotFound();
        return Result<ReviewerDTO>.Success(ReviewerMapper.MapToDTO(reviewer));
    }
}