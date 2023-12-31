using Configuration.Persistence.Entities;
using Configuration.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Configuration.Persistence.Repositories;

public class RepositoryBase<T, TKey, TContext> : 
    RepositoryQueryBase<T, TKey, TContext>, IRepositoryBase<T, TKey, TContext> 
    where T : EntityBase<TKey> where TContext : DbContext
{
    private readonly TContext _dbContext;
    //private readonly IUnitOfWork<TContext> _unitOfWork;

    // public RepositoryBase(TContext dbContext, IUnitOfWork<TContext> unitOfWork) : base(dbContext)
    // {
    //     _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    //     _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    // }
    
    public RepositoryBase(TContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        //_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    

    public void Create(T entity) => _dbContext.Set<T>().Add(entity);

    //public async Task<TKey> CreateAsync(T entity)
    public async Task CreateAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        //await _dbContext.SaveChangesAsync();
        //return entity.Id;
    }

    //public IList<TKey> CreateList(IEnumerable<T> entities)
    public void CreateList(IEnumerable<T> entities)
    {
        _dbContext.Set<T>().AddRange(entities);
        //return entities.Select(x => x.Id).ToList();
    }

    public async Task CreateListAsync(IEnumerable<T> entities)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
        //await _dbContext.SaveChangesAsync();
        //return entities.Select(x => x.Id).ToList();
    }

    public void Update(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;
        
        T exist = _dbContext.Set<T>().Find(entity.Id);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;
        
        T exist = _dbContext.Set<T>().Find(entity.Id);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        //await SaveChangesAsync();
    }

    public void UpdateList(IEnumerable<T> entities) => _dbContext.Set<T>().AddRange(entities);

    public async Task UpdateListAsync(IEnumerable<T> entities)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
        //await SaveChangesAsync();
    }

    public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);
    

    public async Task DeleteAsync(T entity) 
    {
        _dbContext.Set<T>().Remove(entity);
       
        //await SaveChangesAsync();
    }

    public void DeleteList(IEnumerable<T> entities) => _dbContext.Set<T>().RemoveRange(entities);

    public async Task DeleteListAsync(IEnumerable<T> entities)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        //await SaveChangesAsync();
    }

    //public async Task<int> SaveChangesAsync() => await _unitOfWork.CommitAsync();
    //public Task RollbackTransactionAsync() => _dbContext.Database.RollbackTransactionAsync();
    //public Task<IDbContextTransaction> BeginTransactionAsync() => _dbContext.Database.BeginTransactionAsync();

    // public async Task EndTransactionAsync()
    // { 
    //     await SaveChangesAsync();
    //     await _dbContext.Database.CommitTransactionAsync();
    // }
    // Task<int> SaveChangesAsync();
    // Task<IDbContextTransaction> BeginTransactionAsync();
    // Task EndTransactionAsync();
    // Task RollbackTransactionAsync();
}