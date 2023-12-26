using Cms.Infrastructure.Persistence.Entities;
using Configuration.Persistence.Interfaces;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface IProductRepository: IRepositoryBase<Product, long, CmsDbContext>
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductAsync(long id);
    Task<Product> GetProductByNoAsync(string productNo);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(long id);
}