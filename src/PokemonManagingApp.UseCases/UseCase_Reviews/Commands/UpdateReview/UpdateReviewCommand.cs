using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.UpdateReview;

public record UpdateReviewCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Text { get; init; } = null!;
    public string Title { get; init; } = null!;
    public Guid PokemonId { get; init; }
}