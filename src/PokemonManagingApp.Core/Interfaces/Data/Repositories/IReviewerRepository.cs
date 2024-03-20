using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IReviewerRepository : IBaseRepository<Reviewer>
{
    Task<Reviewer?> GetReviewerByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Reviewer>> GetAllReviewersAsync(CancellationToken cancellationToken = default);
}
