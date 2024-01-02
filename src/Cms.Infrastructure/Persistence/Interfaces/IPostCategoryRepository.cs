namespace Cms.Infrastructure.Persistence.Interfaces;

public interface IPostCategoryRepository
{
    Task UpdatePostCatAsync(long postId, List<long> catIds);
}