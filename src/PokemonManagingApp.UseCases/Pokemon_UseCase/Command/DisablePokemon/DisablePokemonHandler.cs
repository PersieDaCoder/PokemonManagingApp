using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;

namespace PokemonManagingApp.UseCases;

public class DisablePokemonHandler(IUnitOfWork unitOfWork) : IRequestHandler<DisablePokemonCommand, Result>
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result> Handle(DisablePokemonCommand request, CancellationToken cancellationToken)
  {
    Core.Models.Pokemon? pokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id == request.Id,true);
    if (pokemon is null)
    {
      return Result.NotFound("pokemon not found");
    }
    // Change the status of the pokemon to false
    pokemon.Status = false;

    await _unitOfWork.SaveChangesAsync();
    return Result.Success();
  }
}
