using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllOwners;

public record GetAllOwnersQuery : IRequest<Result<IEnumerable<OwnerDTO>>>
{
}