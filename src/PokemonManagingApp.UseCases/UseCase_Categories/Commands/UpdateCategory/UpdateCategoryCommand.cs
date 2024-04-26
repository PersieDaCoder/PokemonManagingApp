using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Name { get; init;} = null!;
    public List<Guid> PokemonIds { get; init; } = [];
}