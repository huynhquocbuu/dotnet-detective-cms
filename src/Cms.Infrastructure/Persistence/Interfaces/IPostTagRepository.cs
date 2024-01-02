using Cms.Infrastructure.Persistence.Entities;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface IPostTagRepository
{
    Task<List<PostTag>> FindByPostIdAsync(long postId);
    Task UpdatePostTagAsync(long postId, List<long> tagIds);
}