using System.Linq.Expressions;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Caching;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class BaseRepository<T>(ApplicationDBContext context, ICacheService cacheService) : IBaseRepository<T> where T : class
{
  protected readonly ApplicationDBContext _context = context;
  protected readonly DbSet<T> _dbSet = context.Set<T>();
  protected readonly ICacheService _cacheService = cacheService;

  public void Add(T entity) => _dbSet.Add(entity);
  public void Remove(T entity) => _dbSet.Remove(entity);
  // public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
  public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default)
  => trackChanges ? await _dbSet.ToListAsync(cancellationToken) : await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

  public async Task<IEnumerable<T>> GetEntitiesByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges, CancellationToken cancellationToken = default)
  => trackChanges ? await _dbSet.Where(expression).ToListAsync(cancellationToken) : await _dbSet.Where(expression).AsNoTracking().ToListAsync(cancellationToken);

  public Task<T?> GetEntityByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges, CancellationToken cancellationToken = default)
  => trackChanges ? _dbSet.FirstOrDefaultAsync(expression) : _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);
}
