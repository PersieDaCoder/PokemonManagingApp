using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllReviewsByOwnerId;

public class GetAllReviewByOwnerIdQuery : IRequest<Result<IEnumerable<ReviewDTO>>>
{
    public Guid OwnerId { get; set; }
}