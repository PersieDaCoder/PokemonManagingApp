using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Queries.GetAllReviewers;

public class GetAllReviewersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllReviewersQuery, Result<IEnumerable<ReviewerDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<ReviewerDTO>>> Handle(GetAllReviewersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Reviewer> reviewers = await _unitOfWork.ReviewerRepository.GetAllReviewersAsync(false);
        if (!reviewers.Any()) return Result<IEnumerable<ReviewerDTO>>.NotFound();
        return Result<IEnumerable<ReviewerDTO>>.Success(reviewers.Select(r => ReviewerMapper.MapToDTO(r)));
    }
}