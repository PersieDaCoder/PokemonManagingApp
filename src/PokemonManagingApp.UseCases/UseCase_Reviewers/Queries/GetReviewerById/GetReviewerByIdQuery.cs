using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Reviewers.Queries.GetReviewerById;

public class GetReviewerByIdQuery : IRequest<Result<ReviewerDTO>>
{
    public Guid Id { get; set; }
}