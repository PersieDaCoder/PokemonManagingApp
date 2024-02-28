using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoAPI.Core.Interfaces.Data.Repositories;

public interface IBaseRepository<T> where T : class
{
    void Add(T entity);
    void Remove(T entity);
    // Task SaveChangesAsync();
    Task<IEnumerable<T>> GetAllAsync(bool trackChanges);
    Task<T?> GetEntityByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
    Task<IEnumerable<T>> GetEntitiesByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
}