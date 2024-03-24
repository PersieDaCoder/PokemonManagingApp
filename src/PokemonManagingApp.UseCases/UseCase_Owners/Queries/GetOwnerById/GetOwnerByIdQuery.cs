using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetOwnerById;

  public record GetOwnerByIdQuery : IRequest<Result<OwnerDTO>>
  {
      public Guid Id {get;set;}
  }