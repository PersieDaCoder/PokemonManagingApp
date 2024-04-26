using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.ChangeCountry;

public class ChangeCountryCommand : IRequest<Result>
{
    public Guid OwnerId { get; init; }
    public Guid CountryId { get; init; }
}