using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.DisableOwner;

public class DisableOwnerCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}