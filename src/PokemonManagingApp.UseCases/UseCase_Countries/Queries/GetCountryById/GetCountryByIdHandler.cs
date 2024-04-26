using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetCountryById;

public class GetCountryByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCountryByIdQuery, Result<CountryDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CountryDTO>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        // Get the country by id
        CountryDTO? country =
            await _unitOfWork.CountryRepository
                .GetCountryByIdAsync(request.Id, cancellationToken);
        if (country is null)
            return Result.NotFound("Country is not found");
        return Result.Success(country);
    }
}