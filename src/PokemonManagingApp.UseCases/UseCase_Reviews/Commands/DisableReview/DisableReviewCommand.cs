using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Reviews.Commands.DisableReview;

public class DisableReviewCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}