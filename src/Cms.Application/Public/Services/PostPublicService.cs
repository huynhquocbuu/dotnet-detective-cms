using Cms.Application.Admin.Models.Post;
using Cms.Application.Public.Interfaces;
using Cms.Infrastructure.Persistence.Interfaces;

namespace Cms.Application.Public.Services;

public class PostPublicService : IPostPublicUseCase
{
    private readonly IPostRepository _postRepository;

    public PostPublicService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public PostDetailDto GetPostDetail(string slug)
    {
        return _postRepository
            .FindByCondition(x => x.Slug.Equals(slug))
            .Select(s => new PostDetailDto()
            {
                Content = s.Content
            }).FirstOrDefault();
        
    }
}