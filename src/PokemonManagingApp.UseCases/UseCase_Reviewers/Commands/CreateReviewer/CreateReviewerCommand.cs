using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Commands.CreateReviewer;

public class CreateReviewerCommand : IRequest<Result<ReviewerDTO>>
{
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
}