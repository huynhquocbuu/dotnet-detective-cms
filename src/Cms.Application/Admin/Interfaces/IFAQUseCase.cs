using Cms.Infrastructure.Persistence.Entities;

namespace Cms.Application.Admin.Interfaces;

public interface IFAQUseCase
{
    Task<List<FAQ>> GetAllAsync();
    
    Task<FAQ> GetAsync(long id);
    
    Task EditAsync(FAQ entity);
    Task DeleteAsync(long id);
    
    Task AddAsync(FAQ entity);
}