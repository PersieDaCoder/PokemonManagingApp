using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.DisableCategory;

  public record DisableCategoryCommand : IRequest<Result>
  {
      public Guid Id { get; init; }
  }