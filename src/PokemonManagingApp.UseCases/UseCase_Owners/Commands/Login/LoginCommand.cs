using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.Login;

public class LoginCommand : IRequest<Result<OwnerDTO>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}