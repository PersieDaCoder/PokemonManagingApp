using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.CreateReview;

public class CreateReviewHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateReviewCommand, Result<ReviewDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ReviewDTO>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        Pokemon? checkingPokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id.Equals(request.PokemonId), false);
        if (checkingPokemon is null) return Result.NotFound("Pokemon is not found");
        Review review = new()
        {
            Title = request.Title,
            Text = request.Text,
            PokemonId = request.PokemonId,
            OwnerId = request.OwnerId,
        };
        _unitOfWork.ReviewRepository.Add(review);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success(review.MapToDTO());
    }
}