using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Cms.Infrastructure.Persistence.Repositories;

public class PostTagRepository:IPostTagRepository
{
    private readonly CmsDbContext _context;
    private IPostTagRepository _postTagRepositoryImplementation;

    public PostTagRepository(CmsDbContext context)
    {
        _context = context;
    }

    public Task<List<PostTag>> FindByPostIdAsync(long postId)
    {
        return _context.PostTags.Where(x => x.PostId.Equals(postId)).ToListAsync();
    }

    public async Task UpdatePostTagAsync(long postId, List<long> tagIds)
    {
        var postTags = await _context.PostTags.Where(x => x.PostId.Equals(postId)).ToListAsync();
        _context.PostTags.RemoveRange(postTags);
        
        foreach (var item in tagIds)
        {
            _context.PostTags.Add(new PostTag()
            {
                PostId = postId,
                TagId = item
            });
        }
    }
}