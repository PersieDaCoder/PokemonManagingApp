using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.ChangeCountry;

public class ChangeCountryCommand : IRequest<Result>
{
    public Guid OwnerId { get; set; }
    public Guid CountryId { get; set; }
}