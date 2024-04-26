using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.UpdateOwner;

public record UpdateOwnerCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public Guid GymId { get; init; }
    public required Guid CountryId { get; init; }
}