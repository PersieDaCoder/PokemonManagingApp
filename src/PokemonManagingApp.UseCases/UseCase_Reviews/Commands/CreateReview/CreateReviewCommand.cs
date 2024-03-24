using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.CreateReview;

public class CreateReviewCommand : IRequest<Result<ReviewDTO>>
{
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public Guid PokemonId { get; set; }
}