using Cms.Infrastructure.Persistence.Entities;
using Configuration.Persistence.Interfaces;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface ICategoryRepository : IRepositoryBase<Category, long, CmsDbContext>
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryAsync(long id);
    Task<Category> GetCategoryAsync(long id, bool trackChanges);
}