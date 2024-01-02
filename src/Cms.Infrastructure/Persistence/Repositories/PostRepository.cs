using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Configuration.Persistence.Interfaces;
using Configuration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Persistence.Repositories;

public class PostRepository : RepositoryBase<Post, long, CmsDbContext>, IPostRepository
{
    public PostRepository(CmsDbContext dbContext) : base(dbContext)
    {
    }
   
    //public async Task<IEnumerable<Post>> GetAllAsync() => await FindAll().ToListAsync();

    //public Task<Post> GetByIdAsync(long id) => GetByIdAsync(id);

    public Task<Post> GetByTitleAsync(string title) =>
        FindByCondition(x => x.Title.Equals(title)).SingleOrDefaultAsync();

    //public Task CreateAsync(Post post) => CreateAsync(post);

    //public Task UpdateAsync(Post post) => UpdateAsync(post);

    public async Task DeleteByIdAsync(long id)
    {
        var product = await GetByIdAsync(id);
        if (product != null) await DeleteAsync(product);
    }
}