
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Queries.GetAllReviewers;

public record GetAllReviewersQuery : IRequest<Result<IEnumerable<ReviewerDTO>>>
{

}