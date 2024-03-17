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
    public string Name { get; set; } = null!;
    public string Gym { get; set; } = null!;
    public Guid CountryId { get; set; }
  }