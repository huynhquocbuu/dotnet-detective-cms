using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Configuration.Persistence.Interfaces;
using Configuration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Persistence.Repositories;

public class ProductRepository : RepositoryBase<Product, long, CmsDbContext>, IProductRepository
{
    public ProductRepository(CmsDbContext dbContext) : base(dbContext)
    {
    }
   
    public async Task<IEnumerable<Product>> GetProductsAsync() => await FindAll().ToListAsync();

    public Task<Product> GetProductAsync(long id) => GetByIdAsync(id);

    public Task<Product> GetProductByNoAsync(string productNo) =>
        FindByCondition(x => x.No.Equals(productNo)).SingleOrDefaultAsync();

    public Task CreateProductAsync(Product product) => CreateAsync(product);

    public Task UpdateProductAsync(Product product) => UpdateAsync(product);

    public async Task DeleteProductAsync(long id)
    {
        var product = await GetProductAsync(id);
        if (product != null) await DeleteAsync(product);
    }
}