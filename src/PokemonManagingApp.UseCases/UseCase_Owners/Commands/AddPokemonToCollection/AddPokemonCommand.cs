using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.AddPokemonToCollection;

public record AddPokemonCommand : IRequest<Result<PokemonDTO>>
{
    public Guid OwnerId { get; init; }
    public Guid PokemonId { get; init; }
}