using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Queries.GetAllReviews;

public class GetAllReviewsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllReviewsQuery, Result<IEnumerable<ReviewDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<ReviewDTO>>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ReviewDTO> reviews = await _unitOfWork.ReviewRepository.GetReviewsAsync(cancellationToken);
        if (reviews is null) return Result<IEnumerable<ReviewDTO>>.NotFound();
        return Result<IEnumerable<ReviewDTO>>.Success(reviews);
    }
}