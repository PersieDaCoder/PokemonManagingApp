using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories;

public record GetAllCategoriesQuery : IRequest<Result<IEnumerable<CategoryDTO>>>
{
}
