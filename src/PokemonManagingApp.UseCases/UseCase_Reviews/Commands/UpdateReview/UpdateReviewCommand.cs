using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.UpdateReview;

public class UpdateReviewCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public string Title { get; set; } = null!;
    public Guid ReviewerId { get; set; }
    public Guid PokemonId { get; set; }
}