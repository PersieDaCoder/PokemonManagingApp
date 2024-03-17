using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Commands.DisableReviewer;

public class DisableReviewerHandler(IUnitOfWork unitOfWork) : IRequestHandler<DisableReviewerCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DisableReviewerCommand request, CancellationToken cancellationToken)
    {
        Reviewer? reviewer = await _unitOfWork.ReviewerRepository.GetEntityByConditionAsync(r => r.Id == request.Id, true);
        if (reviewer is null) return Result.NotFound();
        {
            reviewer.Status = false;
        }
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}