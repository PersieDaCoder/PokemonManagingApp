using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
  public Task<Review?> GetReviewByIdAsync(Guid id, bool checkTraces);
  public Task<IEnumerable<Review>> GetAllReviewsAsync(bool checkTraces);
}