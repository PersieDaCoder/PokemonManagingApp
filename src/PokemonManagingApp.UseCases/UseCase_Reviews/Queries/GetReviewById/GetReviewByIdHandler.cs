using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Queries.GetReviewById;

public class GetReviewByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetReviewByIdQuery, Result<ReviewDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ReviewDTO>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        ReviewDTO? review =
            await _unitOfWork.ReviewRepository.GetReviewByIdAsync(request.Id, cancellationToken);
        if (review is null)
            return Result.NotFound("Review is not found");
        return Result.Success(review);
    }
}