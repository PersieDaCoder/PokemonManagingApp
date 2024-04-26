using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Commands.DisableCountry;

public record DisableCountryCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}