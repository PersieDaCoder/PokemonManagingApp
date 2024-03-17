using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetOwnerById;

  public record GetOwnerByIdQuery : IRequest<Result<OwnerDTO>>
  {
      public Guid Id {get;set;}
  }