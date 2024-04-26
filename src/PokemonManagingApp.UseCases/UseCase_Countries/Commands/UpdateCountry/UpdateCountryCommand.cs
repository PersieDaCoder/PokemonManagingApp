using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Commands.UpdateCountry;

public class UpdateCountryCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
}