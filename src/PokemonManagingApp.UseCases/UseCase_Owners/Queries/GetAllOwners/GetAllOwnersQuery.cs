using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllOwners;

public record GetAllOwnersQuery : IRequest<Result<IEnumerable<OwnerDTO>>>
{
}