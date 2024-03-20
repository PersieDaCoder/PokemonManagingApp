using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Queries.GetAllReviews;

public class GetAllReviewsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllReviewsQuery, Result<IEnumerable<ReviewDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<ReviewDTO>>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Review> reviews = await _unitOfWork.ReviewRepository.GetAllReviewsAsync(cancellationToken);
        if (reviews is null) return Result<IEnumerable<ReviewDTO>>.NotFound();
        IEnumerable<ReviewDTO> reviewDTOs = reviews.Select(r => ReviewMapper.MapToDTO(r));
        return Result<IEnumerable<ReviewDTO>>.Success(reviewDTOs);
    }
}