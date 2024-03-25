using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Commands;

public class CreateGymCommand : IRequest<Result<GymDTO>>
{
    public string Name { get; set; } = null!;
}