using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IBaseRepository<T> where T : class
{
  void Add(T entity);
  void Remove(T entity);
  // Task SaveChangesAsync();
  Task<IEnumerable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default);
  Task<T?> GetEntityByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges, CancellationToken cancellationToken = default);
  Task<IEnumerable<T>> GetEntitiesByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges, CancellationToken cancellationToken = default);
}
