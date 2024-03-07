using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.DTOs.Category;
using PokemonManagingApp.Core.DTOs.Country;
using PokemonManagingApp.Core.DTOs.Owner;
using PokemonManagingApp.Core.DTOs.Pokemon;
using PokemonManagingApp.Core.DTOs.Review;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class PokemonRepository : BaseRepository<Pokemon>, IPokemonRepository
{
  public PokemonRepository(ApplicationDBContext context) : base(context)
  {
  }

  public async Task<IEnumerable<Pokemon>> GetAll()
    => await 
    _dbSet
    .AsNoTracking()
    .Include(p => p.Reviews).ThenInclude(r => r.Reviewer)
    .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
    .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
    .ToListAsync();
  /**/
}