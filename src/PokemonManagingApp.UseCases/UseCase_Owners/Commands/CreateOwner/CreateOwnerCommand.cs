using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.CreateOwner;

  public class CreateOwnerCommand : IRequest<Result<OwnerDTO>>
  {
    public string PhoneNumber { get; set; } = null!;
    public string Gmail {get;init;} = null!;
    public string Password {get;init;} = null!;
    public string UserName { get; init; } = null!;
    public string Gym { get; init; } = null!;
    public Guid CountryId { get; init; }
  }