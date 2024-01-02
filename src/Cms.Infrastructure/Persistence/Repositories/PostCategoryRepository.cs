using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Persistence.Repositories;

public class PostCategoryRepository:IPostCategoryRepository
{
    private readonly CmsDbContext _context;
    public PostCategoryRepository(CmsDbContext context)
    {
        _context = context;
    }

    public async Task UpdatePostCatAsync(long postId, List<long> catIds)
    {
        var postCats = await _context.PostCategories.Where(x => x.PostId.Equals(postId)).ToListAsync();
        _context.PostCategories.RemoveRange(postCats);
        
        foreach (var item in catIds)
        {
            _context.PostCategories.Add(new PostCategory()
            {
                PostId = postId,
                CategoryId = item
            });
        }
    }
}