using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Commands.CreateGym;

public class CreateGymCommand : IRequest<Result<GymDTO>>
{
    public string Name { get; init; } = null!;
}