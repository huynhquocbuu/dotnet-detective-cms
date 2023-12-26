using Cms.Infrastructure.Persistence.Entities;
using Configuration.Persistence.Interfaces;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface IFAQRepository : IRepositoryBase<FAQ, long, CmsDbContext>
{
    Task<IEnumerable<FAQ>> GetAllAsync();
    Task<FAQ> GetByIdAsync(long id);
    Task<FAQ> GetByIdAsync(long id, bool trackChanges);
    FAQ GetByQuestion(string question);

}