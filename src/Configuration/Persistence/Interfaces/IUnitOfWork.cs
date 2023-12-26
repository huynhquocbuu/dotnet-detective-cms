using Microsoft.EntityFrameworkCore;

namespace Configuration.Persistence.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
}

public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
}