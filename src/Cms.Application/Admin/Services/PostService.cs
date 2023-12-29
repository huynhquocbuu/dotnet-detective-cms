using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.Post;
using Cms.Infrastructure.Persistence;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Configuration;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

namespace Cms.Application.Admin.Services;

public class PostService : IPostUseCase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IPostRepository _postRepository;
    private readonly CmsConfiguration _cmsConfiguration;
    private readonly CmsDbContext _context;

    public PostService(
        CmsConfiguration cmsConfiguration,
        ICategoryRepository categoryRepository,
        ITagRepository tagRepository,
        IPostRepository postRepository,
        CmsDbContext context)
    {
        _cmsConfiguration = cmsConfiguration;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
        _postRepository = postRepository;
        _context = context;
    }

    public async Task<List<PostDto>> GetAllAsync()
    {
        return _postRepository.FindAll()
            .Include(inc1 => inc1.Categories)
            .Include(inc2 => inc2.Tags)
            .Select(x => new PostDto()
            {
                PostId = x.Id,
                Slug = x.Slug,
                Title = x.Title,
                PostImgUrl = x.ImageUrl,
                Summary = x.Summary,
                Content = x.Content
                
            }).ToList();

    }

    public async Task<PostDto> GetNewPostDto()
    {
        var categories = await _categoryRepository.GetCategoriesAsync();
        var tags = await _tagRepository.GetTagsAsync();
        return new PostDto()
        {
            Categories = categories.Select(x => new CategoryDto()
            {
                Id = x.Id,
                Title = x.Title
            }).ToList(),
            SelectedCategories = categories
                .Where(c => c.Slug.Equals("tham-tu"))
                .Select(s => s.Id).ToList(),
            Tags = tags.Select(t => new TagDto()
            {
                Title = t.Title,
                Id = t.Id
            }).ToList(),
            SelectedTags = tags
                .Where(c =>c.Slug.Equals("tham-tu"))
                .Select(s => s.Id).ToList(),
            Content = "Nhập nội dung bài viết",
            Summary = "Nhập Summary",
            
        };
    }

    public async Task AddPost(PostDto dto, string authorId)
    {
        //Upload image
        dto.PostImgUrl = Path.Combine(
            _cmsConfiguration.WebRootPath,
            _cmsConfiguration.ContentFolder,
            _cmsConfiguration.BlogFolder,
            dto.PostImg.FileName);
        using (FileStream stream = new FileStream(dto.PostImgUrl, FileMode.Create))
        {
            await dto.PostImg.CopyToAsync(stream);
        }
        
        //insert Db
        var entity = await ConvertToPostEntity(dto);
        entity.Author = authorId;
        await _postRepository.CreateAsync(entity);
    }

    private async Task<Post> ConvertToPostEntity(PostDto dto)
    {
        var categories = new List<Category>();
        foreach (var id in dto.SelectedCategories)
        {
            categories.Add(await _categoryRepository.GetCategoryAsync(id, true));
        }

        var tags = new List<Tag>();
        foreach (var id in dto.SelectedTags)
        {
            tags.Add(await _tagRepository.GetTagAsync(id, true));
        }
        return new Post()
        {
            Categories = categories,
            Tags = tags,
            Summary = dto.Summary,
            Title = dto.Title,
            Slug = dto.Slug,
            Content = dto.Content,
            ImageUrl = dto.PostImgUrl,
            IsPublished = true,
        };
        
    }

    public async Task<PostDto> GetEditPostDto(long id)
    {
        var postEntity = await _postRepository.FindByCondition(x => x.Id.Equals(id))
            .Include(incl1 => incl1.Categories)
            .Include(incl2 => incl2.Tags)
            .FirstOrDefaultAsync();
        //postEntity.Categories

        var categories = await _categoryRepository.GetCategoriesAsync();
        var tags = await _tagRepository.GetTagsAsync();
        return new PostDto()
        {
            PostId = id,
            Title = postEntity.Title,
            Slug = postEntity.Slug,
            PostImgUrl = postEntity.ImageUrl,
            Categories = categories.Select(x => new CategoryDto()
            {
                Id = x.Id,
                Title = x.Title
            }).ToList(),
            SelectedCategories = postEntity.Categories
                .Select(s => s.Id).ToList(),
            Tags = tags.Select(t => new TagDto()
            {
                Title = t.Title,
                Id = t.Id
            }).ToList(),
            SelectedTags = postEntity.Tags
                .Select(s => s.Id).ToList(),
            Content = postEntity.Content,
            Summary = postEntity.Summary,

        };
    }

    public async Task<int> DoEdit(PostDto model)
    {
        var postEntity = await _postRepository
            .FindByCondition(x => x.Id == model.PostId, false)
           .Include(incl1 => incl1.Categories)
            .Include(incl2 => incl2.Tags)
            .FirstOrDefaultAsync();
        
        //postEntity.Categories = new List<Category>();
        //postEntity.Tags = new List<Tag>();
        postEntity.Categories.Clear(); postEntity.Tags.Clear();
        foreach (var item in model.SelectedCategories)
        {
            var cat = await _categoryRepository.GetByIdAsync(item, false);
            postEntity.Categories.Add(cat);
        }
        foreach (var item in model.SelectedTags)
        {
            var tag = await _tagRepository.GetByIdAsync(item, false);
            postEntity.Tags.Add(tag);
        }

        postEntity.Slug = model.Slug;
        postEntity.Title = model.Title;
        postEntity.Summary = model.Summary;
        postEntity.Content = model.Content;

        //_context.ChangeTracker.Clear();
        _postRepository.Update(postEntity);

       _context.SaveChanges();
        //return await _postRepository.SaveChangesAsync();
        return 0;
    }
}