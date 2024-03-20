using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.RemovePokemonOutOfCategory;

public record RemovePokemonOutOfCategoryCommand : IRequest<Result>
{
    public Guid CategoryId { get; init; }
    public Guid PokemonId { get; init; }
}