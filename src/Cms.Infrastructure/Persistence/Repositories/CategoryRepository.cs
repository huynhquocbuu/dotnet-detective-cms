using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Configuration.Persistence.Interfaces;
using Configuration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Persistence.Repositories;

public class CategoryRepository : RepositoryBase<Category, long, CmsDbContext>, ICategoryRepository
{
    public CategoryRepository(CmsDbContext dbContext, IUnitOfWork<CmsDbContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await FindAll().ToListAsync();
    }

    public async Task<Category> GetCategoryAsync(long id, bool trackChanges) => 
        await GetByIdAsync(id: id, trackChanges: trackChanges);
    public async Task<Category> GetCategoryAsync(long id) => await GetByIdAsync(id);
    
}