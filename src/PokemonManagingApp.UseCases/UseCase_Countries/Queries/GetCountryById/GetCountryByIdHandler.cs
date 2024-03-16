using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetCountryById;

public class GetCountryByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCountryByIdQuery, Result<CountryDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CountryDTO>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        CountryDTO? country = await _unitOfWork.CountryRepository.DBSet()
            .AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Where(c => c.Status)
            .Select(c => CountryMapper.MapToDTO(c))
            .FirstOrDefaultAsync();
        if (country is null) return Result<CountryDTO>.NotFound();
        return Result<CountryDTO>.Success(country);
    }
}