using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Queries.GetReviewById;

public record GetReviewByIdQuery : IRequest<Result<ReviewDTO>>
{
    public Guid Id { get; init; }
}