using Cms.Infrastructure.Persistence.Entities;
using Configuration.Persistence.Interfaces;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface IPostRepository: IRepositoryBase<Post, long, CmsDbContext>
{
    Task<IEnumerable<Post>> GetProductsAsync();
    Task<Post> GetProductAsync(long id);
    Task<Post> GetProductByTitleAsync(string title);
    Task CreateProductAsync(Post post);
    Task UpdateProductAsync(Post post);
    Task DeleteProductAsync(long id);
}