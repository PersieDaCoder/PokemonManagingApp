using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoAPI.Core.Interfaces.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Infrastructure.Data.Repository;

public class BaseRepository<T>(ApplicationDBContext context) : IBaseRepository<T> where T : class
{
    protected readonly ApplicationDBContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();
    
    public void Add(T entity) => _dbSet.Add(entity);
    public void Remove(T entity) => _dbSet.Remove(entity);
    // public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges)
    => trackChanges ? await _dbSet.ToListAsync() : await _dbSet.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<T>> GetEntitiesByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
    => trackChanges ? await _dbSet.Where(expression).ToListAsync() : await _dbSet.Where(expression).AsNoTracking().ToListAsync();

    public Task<T?> GetEntityByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
    => trackChanges ? _dbSet.FirstOrDefaultAsync(expression) : _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);
}