using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.CreateReview;

public record CreateReviewCommand : IRequest<Result<ReviewDTO>>
{
    public string Title { get; init; } = null!;
    public string Text { get; init; } = null!;
    public Guid OwnerId { get; init; }
    public Guid PokemonId { get; init; }
}