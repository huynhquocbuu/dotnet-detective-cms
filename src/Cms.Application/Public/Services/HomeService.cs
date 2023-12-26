using Cms.Application.Public.Interfaces;
using Cms.Application.Public.Models;
using Cms.Infrastructure.Persistence;
using Cms.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Cms.Application.Public.Services;

public class HomeService : IHomeUseCase
{
    private readonly IPostRepository _postRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;
    private readonly CmsDbContext _cmsDbContext;

    public HomeService(
        IPostRepository postRepository, 
        ICategoryRepository categoryRepository,
        ITagRepository tagRepository,
        CmsDbContext cmsDbContext)
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
        _cmsDbContext = cmsDbContext;
    }

    public HomeInfoDto GetAboutUsInfo()
    {
        return _postRepository
            .FindByCondition(x => x.Slug.Equals("about-us.html"))
            .Select(s => new HomeInfoDto()
            {
                Title = s.Title,
                DetailSlug = s.Slug,
                Content = s.Summary,
                Session = "AboutUs"
            })
            .FirstOrDefault();
    }

    public List<HomeInfoDto> GetServicesInfo()
    {
        var posts = _cmsDbContext.Posts
            .FromSqlRaw(@"
                SELECT p.* FROM Posts p
                JOIN CategoryPost cp ON p.Id = cp.PostsId
                JOIN Categories c ON c.Id = cp.CategoriesId
                JOIN PostTag pt ON pt.PostsId = p.Id
                JOIN Tags t ON pt.TagsId = t.Id
                WHERE t.Slug = 'services' and c.Slug = 'home'
                --ORDER BY p.[Order]")
            .OrderBy(o => o.Order)
            .ToList();
        return posts
            .Select(s => new HomeInfoDto()
            {
                Title = s.Title,
                DetailSlug = s.Slug,
                Content = s.Summary,
                Session = "Services"
            }).ToList();
    }

    public List<HomeInfoDto> GetPostsInfo()
    {
        var posts = _cmsDbContext.Posts
            .FromSqlRaw(@"
                SELECT p.* FROM Posts p
                JOIN CategoryPost cp ON p.Id = cp.PostsId
                JOIN Categories c ON c.Id = cp.CategoriesId
                JOIN PostTag pt ON pt.PostsId = p.Id
                JOIN Tags t ON pt.TagsId = t.Id
                WHERE c.Slug = 'tham-tu'")
            .OrderBy(o => o.Order)
            .ToList();
        
        return posts
            .Select(s => new HomeInfoDto()
            {
                Title = s.Title,
                DetailSlug = s.Slug,
                Content = s.Summary,
                ImageUrl = s.ImageUrl,
                Session = "Post"
            }).ToList();
    }
}