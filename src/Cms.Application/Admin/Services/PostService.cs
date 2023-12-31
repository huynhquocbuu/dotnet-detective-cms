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
    private readonly CmsConfiguration _cmsConfiguration;
    private readonly CmsDbContext _context;
    private readonly IUnitOfWork _uow;
    public PostService(
        CmsConfiguration cmsConfiguration,
        CmsDbContext context,
        IUnitOfWork uow)
    {
        _cmsConfiguration = cmsConfiguration;
        _context = context;
        _uow = uow;
    }

    public async Task<List<PostDto>> GetAllAsync()
    {
        return _uow.PostRepository.FindAll()
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
        var categories = await _uow.CategoryRepository.GetCategoriesAsync();
        var tags = await _uow.TagRepository.GetTagsAsync();
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
        dto.PostImgUrl = $"/{_cmsConfiguration.ContentFolder}/{_cmsConfiguration.BlogFolder}/{dto.PostImg.FileName}";
        var uploadPath = Path.Combine(
            _cmsConfiguration.WebRootPath,
            _cmsConfiguration.ContentFolder,
            _cmsConfiguration.BlogFolder,
            dto.PostImg.FileName);
        using (FileStream stream = new FileStream(uploadPath, FileMode.Create))
        {
            await dto.PostImg.CopyToAsync(stream);
        }
        
        //insert Db
        var entity = await ConvertToPostEntity(dto);
        entity.Author = authorId;
        await _uow.PostRepository.CreateAsync(entity);
        await _uow.CommitAsync();
    }

    private async Task<Post> ConvertToPostEntity(PostDto dto)
    {
        var categories = new List<Category>();
        foreach (var id in dto.SelectedCategories)
        {
            categories.Add(await _uow.CategoryRepository.GetCategoryAsync(id, true));
        }

        var tags = new List<Tag>();
        foreach (var id in dto.SelectedTags)
        {
            tags.Add(await _uow.TagRepository.GetTagAsync(id, true));
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
        var postEntity = await _uow.PostRepository.FindByCondition(x => x.Id.Equals(id))
            .Include(incl1 => incl1.Categories)
            .Include(incl2 => incl2.Tags)
            .FirstOrDefaultAsync();

        var categories = await _uow.CategoryRepository.GetCategoriesAsync();
        var tags = await _uow.TagRepository.GetTagsAsync();
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
        var postEntity = await _uow.PostRepository
            .FindByCondition(x => x.Id == model.PostId, true)
            .FirstOrDefaultAsync();

        postEntity.Slug = model.Slug;
        postEntity.Title = model.Title;
        postEntity.Summary = model.Summary;
        postEntity.Content = model.Content;
        
        //Upload image
        if (model.PostImg != null)
        {
            var uploadUrl = Path.Combine(
                _cmsConfiguration.WebRootPath,
                _cmsConfiguration.ContentFolder,
                _cmsConfiguration.BlogFolder,
                model.PostImg.FileName);
            using (FileStream stream = new FileStream(uploadUrl, FileMode.Create))
            {
                await model.PostImg.CopyToAsync(stream);
            }
            model.PostImgUrl = $"/{_cmsConfiguration.ContentFolder}/{_cmsConfiguration.BlogFolder}/{model.PostImg.FileName}";
            postEntity.ImageUrl = model.PostImgUrl;
        }
        await _uow.PostCategoryRepository.UpdatePostCatAsync(model.PostId, model.SelectedCategories);
        await _uow.PostTagRepository.UpdatePostTagAsync(model.PostId, model.SelectedTags);
        
        return await _context.SaveChangesAsync();
    }
}