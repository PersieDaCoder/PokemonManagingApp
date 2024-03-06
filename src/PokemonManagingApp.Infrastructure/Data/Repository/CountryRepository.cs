using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CountryRepository : BaseRepository<Country>, ICountryRepository
{
  public CountryRepository(ApplicationDBContext context) : base(context)
  {
  }
}