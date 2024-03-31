using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Queries.GetGymById;

public record GetGymByIdQuery : IRequest<Result<GymDTO>>
{
    public Guid Id { get; init; }
}