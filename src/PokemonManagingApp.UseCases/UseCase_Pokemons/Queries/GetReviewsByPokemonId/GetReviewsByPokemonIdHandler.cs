using Ardalis.Result;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons.Queries.GetReviewsByPokemonId;

public class GetReviewsByPokemonIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetReviewsByPokemonIdQuery, Result<IEnumerable<ReviewDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<ReviewDTO>>> Handle(GetReviewsByPokemonIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Review> reviews = await _unitOfWork.ReviewRepository.GetReviewsByPokemonIdAsync(request.PokemonId, cancellationToken);
        if (reviews.IsNullOrEmpty()) return Result.NotFound("Reviews is not found");
        return Result.Success(reviews.Select(r => r.MapToDTO()));
    }
}