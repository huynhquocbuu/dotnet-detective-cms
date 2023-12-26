using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Configuration.Persistence.Interfaces;
using Configuration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Persistence.Repositories;

public class PostRepository : RepositoryBase<Post, long, CmsDbContext>, IPostRepository
{
    public PostRepository(CmsDbContext dbContext, IUnitOfWork<CmsDbContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }
   
    public async Task<IEnumerable<Post>> GetProductsAsync() => await FindAll().ToListAsync();

    public Task<Post> GetProductAsync(long id) => GetByIdAsync(id);

    public Task<Post> GetProductByTitleAsync(string title) =>
        FindByCondition(x => x.Title.Equals(title)).SingleOrDefaultAsync();

    public Task CreateProductAsync(Post post) => CreateAsync(post);

    public Task UpdateProductAsync(Post post) => UpdateAsync(post);

    public async Task DeleteProductAsync(long id)
    {
        var product = await GetProductAsync(id);
        if (product != null) await DeleteAsync(product);
    }
}