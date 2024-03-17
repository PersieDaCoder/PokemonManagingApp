using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Commands.DisableReviewer;

public class DisableReviewerCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}