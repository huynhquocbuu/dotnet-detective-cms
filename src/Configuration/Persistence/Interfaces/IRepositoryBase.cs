
using System.Linq.Expressions;
using Configuration.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Configuration.Persistence.Interfaces;

public interface IRepositoryQueryBase<T, in TKey> 
    where T : EntityBase<TKey>
{
    IQueryable<T> FindAll(bool trackChanges = false);
    IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
        params Expression<Func<T, object>>[] includeProperties);

    Task<T?> GetByIdAsync(TKey id);
    Task<T?> GetByIdAsync(TKey id, bool trackChanges);
    Task<T?> GetByIdAsync(TKey id, params Expression<Func<T, object>>[] includeProperties);
}

public interface IRepositoryQueryBase<T, TKey, TContext> : IRepositoryQueryBase<T, TKey> 
    where T : EntityBase<TKey>
    where TContext : DbContext
{
}

public interface IRepositoryBase<T, K> : IRepositoryQueryBase<T, K>
    where T : EntityBase<K>
{
    void Create(T entity);
    Task CreateAsync(T entity);
    //IList<K> CreateList(IEnumerable<T> entities);
    void CreateList(IEnumerable<T> entities);
    Task CreateListAsync(IEnumerable<T> entities);
    void Update(T entity);
    Task UpdateAsync(T entity);
    void UpdateList(IEnumerable<T> entities);
    Task UpdateListAsync(IEnumerable<T> entities);
    void Delete(T entity);
    //void Delete(K id);
    Task DeleteAsync(T entity);
    void DeleteList(IEnumerable<T> entities);
    Task DeleteListAsync(IEnumerable<T> entities);
    
    // Task<int> SaveChangesAsync();
    // Task<IDbContextTransaction> BeginTransactionAsync();
    // Task EndTransactionAsync();
    // Task RollbackTransactionAsync();
}

public interface IRepositoryBase<T, TKey, TContext> : IRepositoryBase<T, TKey>
    where T : EntityBase<TKey>
    where TContext : DbContext
{
}