using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.AddPokemonToCollection;

public class AddPokemonCommand : IRequest<Result<PokemonDTO>>
{
    public Guid OwnerId { get; set; }
    public Guid PokemonId { get; set; }
}