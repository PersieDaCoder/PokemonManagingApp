using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.DisableReview;

public record DisableReviewCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}