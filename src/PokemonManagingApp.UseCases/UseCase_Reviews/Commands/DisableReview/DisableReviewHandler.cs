using System;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.DisableReview;

public class DisableReviewHandler(IUnitOfWork unitOfWork) : IRequestHandler<DisableReviewCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DisableReviewCommand request, CancellationToken cancellationToken)
    {
        Review? checkingReview =
            await _unitOfWork.ReviewRepository
                .GetEntityByConditionAsync(r => r.Id.Equals(request.Id), true);
        if (checkingReview is null)
            return Result.NotFound("Review is not found");
        checkingReview.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessWithMessage("Review is disabled successfully");
    }
}