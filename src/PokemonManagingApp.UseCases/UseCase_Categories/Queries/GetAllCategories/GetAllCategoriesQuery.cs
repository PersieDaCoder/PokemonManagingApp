using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories;

public record GetAllCategoriesQuery : IRequest<Result<IEnumerable<CategoryDTO>>>
{

}
