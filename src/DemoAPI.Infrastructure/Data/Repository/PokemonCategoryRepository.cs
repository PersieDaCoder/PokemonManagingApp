using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.Interfaces.Data.Repositories;
using DemoAPI.Core.Models;

namespace DemoAPI.Infrastructure.Data.Repository;

public class PokemonCategoryRepository : BaseRepository<PokemonCategory>, IPokemonCategoryRepository
{
  public PokemonCategoryRepository(ApplicationDBContext context) : base(context)
  {
  }
}