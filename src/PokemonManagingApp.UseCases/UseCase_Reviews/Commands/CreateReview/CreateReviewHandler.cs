using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.CreateReview;

public class CreateReviewHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateReviewCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        Pokemon? checkingPokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id.Equals(request.PokemonId), false);
        if (checkingPokemon is null) return Result.NotFound();
        Reviewer? checkingReviewer = await _unitOfWork.ReviewerRepository.GetEntityByConditionAsync(r => r.Id.Equals(request.ReviewerId), false);
        var review = new Review
        {
            Title = request.Title,
            Text = request.Text,
            ReviewerId = request.ReviewerId,
            PokemonId = request.PokemonId,
        };
        _unitOfWork.ReviewRepository.Add(review);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}