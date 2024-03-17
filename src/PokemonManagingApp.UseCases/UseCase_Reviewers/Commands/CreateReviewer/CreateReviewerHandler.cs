using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Commands.CreateReviewer;

public class CreateReviewerHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateReviewerCommand, Result<ReviewerDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ReviewerDTO>> Handle(CreateReviewerCommand request, CancellationToken cancellationToken)
    {
        Reviewer addingReviewer = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        _unitOfWork.ReviewerRepository.Add(addingReviewer);
        await _unitOfWork.SaveChangesAsync();
        return Result<ReviewerDTO>.Success(ReviewerMapper.MapToDTO(addingReviewer));
    }
}