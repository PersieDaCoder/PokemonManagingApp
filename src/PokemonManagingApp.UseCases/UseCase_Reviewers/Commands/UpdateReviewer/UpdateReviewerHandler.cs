using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Commands.UpdateReviewer;

public class UpdateReviewerHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateReviewerCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateReviewerCommand request, CancellationToken cancellationToken)
    {
        Reviewer? reviewer = await _unitOfWork.ReviewerRepository.GetEntityByConditionAsync(r => r.Id == request.Id, true);
        if (reviewer is null) return Result.NotFound();
        {
            reviewer.FirstName = request.FirstName;
            reviewer.LastName = request.LastName;
        }
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}