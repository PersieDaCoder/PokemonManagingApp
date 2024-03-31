using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Queries.GetAllReviews;

  public record GetAllReviewsQuery : IRequest<Result<IEnumerable<ReviewDTO>>>
  {}