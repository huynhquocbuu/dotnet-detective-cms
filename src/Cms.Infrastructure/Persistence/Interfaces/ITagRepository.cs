using Cms.Infrastructure.Persistence.Entities;
using Configuration.Persistence.Interfaces;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface ITagRepository : IRepositoryBase<Tag, long, CmsDbContext>
{
    Task<IEnumerable<Tag>> GetTagsAsync();
    Task<Tag> GetTagAsync(long id);
    Task<Tag> GetTagAsync(long id, bool trackChanges);
}