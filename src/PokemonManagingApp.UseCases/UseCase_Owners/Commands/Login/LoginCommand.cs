using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.Login;

public class LoginCommand : IRequest<Result<OwnerDTO>>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}