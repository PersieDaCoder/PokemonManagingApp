using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Commands.UpdateReviewer;

public class UpdateReviewerCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}