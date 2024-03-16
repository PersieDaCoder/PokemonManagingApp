using PokemonManagingApp.Core.Interfaces.Data;
using MediatR;
using Ardalis.Result;

namespace PokemonManagingApp.Pokemon_UseCase;

public class UpdatePokemonHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePokemonCommand, Result>
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result> Handle(UpdatePokemonCommand request, CancellationToken cancellationToken)
  {
    Core.Models.Pokemon? pokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id == request.Id, true);
    if (pokemon is null) return Result.NotFound();
    {
      pokemon.Name = request.Name;
      pokemon.BirthDate = request.BirthDate;
    }
    await _unitOfWork.SaveChangesAsync();
    return Result.Success();
  }

}