using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.RemovePokemonOutOfCollection;

public class RemovePokemonOutOfCollectionCommand : IRequest<Result>
{
    public Guid OwnerId { get; set; }
    public Guid PokemonId { get; set; }
}