using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.UpdateReview;

public class UpdateReviewHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateReviewCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        Review? checkingReview = await _unitOfWork.ReviewRepository.GetEntityByConditionAsync(r => r.Id.Equals(request.Id), true);
        if (checkingReview is null) return Result.NotFound("Review not found");
        Pokemon? checkingPokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id.Equals(request.PokemonId), false);
        if (checkingPokemon is null) return Result.NotFound("Pokemon not found");
        {
            checkingReview.Title = request.Title;
            checkingReview.Text = request.Text;
            checkingReview.PokemonId = request.PokemonId;
        }
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}