using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllReviewsByOwnerId;

public record GetAllReviewByOwnerIdQuery : IRequest<Result<IEnumerable<ReviewDTO>>>
{
    public Guid OwnerId { get; init; }
}