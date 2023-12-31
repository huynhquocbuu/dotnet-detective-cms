using System.Linq.Expressions;
using Configuration.Persistence.Entities;
using Configuration.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Configuration.Persistence.Repositories;


public class RepositoryQueryBase<T, TKey> where T : EntityBase<TKey>
{
}
public class RepositoryQueryBase<T, TKey, TContext> : 
    RepositoryQueryBase<T, TKey>, IRepositoryQueryBase<T, TKey, TContext>
    where T: EntityBase<TKey> where TContext: DbContext
{
    private readonly TContext _dbContext;

    public RepositoryQueryBase(TContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public IQueryable<T> FindAll(bool trackChanges = false) => 
        !trackChanges ? _dbContext.Set<T>().AsNoTracking() : 
            _dbContext.Set<T>();

    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindAll(trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) => 
        !trackChanges
            ? _dbContext.Set<T>().Where(expression).AsNoTracking()
            : _dbContext.Set<T>().Where(expression);

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindByCondition(expression, trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public async Task<T?> GetByIdAsync(TKey id) => 
        await FindByCondition(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();
    public async Task<T?> GetByIdAsync(TKey id, bool trackChanges) => 
        await FindByCondition(x => x.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();

    public async Task<T> GetByIdAsync(TKey id, params Expression<Func<T, object>>[] includeProperties) => 
        await FindByCondition(x => x.Id.Equals(id), trackChanges:false, includeProperties)
            .FirstOrDefaultAsync();
}