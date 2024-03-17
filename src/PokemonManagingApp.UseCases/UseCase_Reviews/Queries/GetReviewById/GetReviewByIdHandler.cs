using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Queries.GetReviewById;

public class GetReviewByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetReviewByIdQuery, Result<ReviewDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ReviewDTO>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        Review? review = await _unitOfWork.ReviewRepository.GetReviewByIdAsync(request.Id, false);
        if (review is null) return Result<ReviewDTO>.NotFound();
        ReviewDTO reviewDTO = ReviewMapper.MapToDTO(review);
        return Result<ReviewDTO>.Success(reviewDTO);
    }
}