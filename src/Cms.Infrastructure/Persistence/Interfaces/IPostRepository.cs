using Cms.Infrastructure.Persistence.Entities;
using Configuration.Persistence.Interfaces;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface IPostRepository : IRepositoryBase<Post, long, CmsDbContext>
{
    //Task<IEnumerable<Post>> GetAllAsync();
    //Task<Post> GetByIdAsync(long id);
    Task<Post> GetByTitleAsync(string title);
    //Task CreateAsync(Post post);
    //Task UpdateAsync(Post post);
    Task DeleteByIdAsync(long id);
}