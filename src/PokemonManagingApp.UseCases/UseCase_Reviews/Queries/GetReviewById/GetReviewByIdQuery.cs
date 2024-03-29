using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Queries.GetReviewById;

public class GetReviewByIdQuery : IRequest<Result<ReviewDTO>>
{
    public Guid Id { get; init; }
}