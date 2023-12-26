using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Configuration.Persistence.Interfaces;
using Configuration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Persistence.Repositories;

public class TagRepository: RepositoryBase<Tag, long, CmsDbContext>, ITagRepository
{
    public TagRepository(CmsDbContext dbContext, IUnitOfWork<CmsDbContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<IEnumerable<Tag>> GetTagsAsync()
    {
        return await FindAll().ToListAsync();
    }
    
    public async Task<Tag> GetTagAsync(long id, bool trackChanges) => 
        await GetByIdAsync(id: id, trackChanges: trackChanges);
    public async Task<Tag> GetTagAsync(long id) => await GetByIdAsync(id);
}