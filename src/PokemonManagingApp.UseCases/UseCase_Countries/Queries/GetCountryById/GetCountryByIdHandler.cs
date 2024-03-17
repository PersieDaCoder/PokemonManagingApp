using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetCountryById;

public class GetCountryByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCountryByIdQuery, Result<CountryDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CountryDTO>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        Country? country = await _unitOfWork.CountryRepository.GetEntityByConditionAsync(c => c.Id.Equals(request.Id), false);
        if (country is null) return Result<CountryDTO>.NotFound();
        return Result<CountryDTO>.Success(CountryMapper.MapToDTO(country));
    }
}